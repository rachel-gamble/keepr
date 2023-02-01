<template>
    <div class="container-fluid app-bg oxygen">
        <!--SECTION Top info-->

        <section class="row d-flex justify-content-center text-center">
            <img :src="profile?.coverImg" class="banner-img" alt="profile cover image" title="Profile cover image">
            <img :src="profile?.picture" alt="profile picture" class="profile-icon">
            <div class="flex-column">
                <h1>{{ profile?.name }}</h1>
                <div class="counts">
                    <span class="bold">{{ numberOfVaults }} Vaults</span>
                    <span class="mx-1">|</span>
                    <span class="bold">Keeps: {{ numberOfKeeps }}</span>
                </div>
            </div>
        </section>

        <!--SECTION Vaults-->
        <section class="row d-flex justify-content-center">
            <div class="col-10">
                <h3 class="sub-title fw-bold mx-3 my-1">Vaults</h3>
                <!--v-for-->
                <div class="masonry-container">
                    <div v-for="vault in vaults" :key="vault.id">
                        <div class="" @click="goToVault(vault)">
                            <div class="rounded my-2 mb-2 elevation-5 hover-card keep-container vault-card image-custom image-fluid selectable hover-shadow"
                                :style="`background-image: url(${vault?.img})`"
                                :title="'Go to ' + vault?.name + ' by ' + vault?.creatorId.name">
                                <div class="d-flex justify-content-between-mobile-cleanup">
                                    <h4 class="vault-name">
                                        {{ vault?.name }}
                                    </h4>
                                </div>
                                <br><br>
                            </div>
                        </div>
                    </div>
                </div>
                <!--end v-for-->
            </div>
        </section>

        <br /><br /><br /><br /><br /><br />

        <!--SECTION Keeps-->
        <section class="row d-flex justify-content-center">
            <div class="col-10">
                <h3 class="sub-title fw-bold mx-3 my-1 mb-3">Keeps</h3>
                <!--v-for--> <!-- Keep Masonry -->
                <div class="masonry-container">
                    <div v-for="k in keeps" :key="k.id">
                        <div class="" @click="openKeepDetails(k)">
                            <div class="rounded my-2 mb-2 elevation-5 hover-card keep-container vault-card image-custom image-fluid selectable hover-shadow"
                                :style="`background-image: url(${k?.img})`"
                                :title="'Open ' + keep?.name + ' by ' + vault?.creatorId.name">
                                <div class="d-flex justify-content-between-mobile-cleanup">
                                    <h4 class="vault-name">
                                        {{ k?.name }}
                                    </h4>
                                </div>
                                <br><br>
                            </div>
                        </div>
                    </div>
                </div>
                <!--end v-for-->
            </div>
        </section>
    </div>
</template>

<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { watchEffect } from '@vue/runtime-core'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { profilesService } from '../services/ProfilesService'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { useRoute, useRouter } from 'vue-router'
import { Modal } from 'bootstrap'
import KeepDetails from '../components/KeepDetails.vue'

export default {
    props: {
        vault: { type: Object, required: true },
        keep: { type: Object, required: true },
    },
    setup(props) {
        const route = useRoute();
        const router = useRouter();
        watchEffect(async () => {
            try {
                if (route.params.id && route.name == "Profile") {
                    await profilesService.getUserProfile(route.params.id);
                    await keepsService.getUserKeeps(route.params.id);
                    await vaultsService.getUserVaults(route.params.id);
                }
            } catch (error) {
                logger.error(error)
                Pop.toast(error.message, 'error')
            }
        })
        return {
            route,
            account: computed(() => AppState.account),
            profile: computed(() => AppState.activeProfile),
            keeps: computed(() => AppState.keeps),
            vaults: computed(() => AppState.userVaults),
            numberOfKeeps: computed(() => AppState.keeps.length),
            numberOfVaults: computed(() => AppState.userVaults.length),
            async openKeepModal(k) {
                AppState.activeKeep = k;
                Modal.getOrCreateInstance(document.getElementById('keep-modal')).show()
                await keepsService.incrementViews();
            },

            async goToVault(v) {
                const userId = AppState.account.id
                console.log(userId, v.creatorId)
                if (v.creatorId.toString() != userId.toString() && v.isPrivate == true) {
                    router.push({ name: 'Home' });
                    Pop.toast("You do not have access to this vault. 🔒")
                    return
                }
                AppState.activeVault = v
                router.push({ name: 'Vault', params: { id: v.id } })
            },

            async openKeepDetails(k) {
                AppState.activeKeep = k;
                Modal.getOrCreateInstance(document.getElementById('keepDetails')).show()
                await keepsService.incrementViews();
            },

            createVault() {
                Modal.getOrCreateInstance(document.getElementById("new-vault-modal")).show()
            },
            createKeep() {
                Modal.getOrCreateInstance(document.getElementById('new-keep-modal')).show()
            }
        };

    },
    components: { KeepDetails, },
}
</script>

<style>
.oxygen {
    font-family: 'Oxygen', sans-serif;
}

.profile-icon {
    width: 20vw;
    height: 20vh;
    /* border:1px solid; */
    border-radius: 50%;
    object-fit: cover;
    justify-content: center;
    align-items: center;
    transform: translateY(-10vh);
    margin-bottom: -8vh;
}

.banner-img {
    width: 85vw;
    height: 50vh;
    object-fit: cover;
}

.vault {
    color: whitesmoke;
    text-shadow: 3px 3px 4px black;
    animation-name: fadeInto;
    animation-duration: 5000ms;
}

.masonry-container-profile {
    column-count: 6;
    column-gap: 0.5em;
}

@media only screen and (max-width: 1068px) {
    .masonry-container-profile {
        column-count: 4;
        column-gap: 0.5em;
    }
}

@media only screen and (max-width: 768px) {
    .masonry-container-profile {
        column-count: 2;
        column-gap: 0.5em;
    }
}

@media only screen and (max-width: 425px) {
    .masonry-container-profile {
        column-count: 1;
    }
}

.img-clamp {
    height: 6em;
    width: 6em;
}

.plus {
    text-shadow: 2px 2px 2px black;
    cursor: pointer;
}
</style>