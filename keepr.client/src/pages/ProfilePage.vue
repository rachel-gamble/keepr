<template>
    <div class="container-fluid app-bg">
        <!--SECTION Top info-->

        <section class="row d-flex">
            <img :src="profile?.picture" alt="profile picture" class="img-fluid">
            <div class="flex-column">
                <h1>{{ profile?.name }}</h1>
                <span>Vaults: {{ numberOfVaults }}</span>
                <span>Keeps: {{ numberOfKeeps }}</span>
            </div>
        </section>

        <!--SECTION Vaults-->
        <section class="row d-flex justify-content-center">
            <div class="col-8 justify-items-center">
                <h3 class="sub-title fw-bold">Vaults</h3><br>

                <div v-for="v in vaults" :key="v.id" :message="vault">
                    <VaultCard :vault="v" />
                </div>
                <!-- <VaultCard v-for="v in vaults" :key="v.id" class="card col-4 m-2 d-flex">

                </VaultCard> -->

            </div>
            <div class="col-8 justify-items-center">
                <h3 class="sub-title fw-bold">Keeps</h3><br>
                <div v-for="v in vaults" :key="v.id" class="col-sm-6 col-md-4 col-lg-2 p-2"></div>
            </div>
        </section>





    </div>

    <!--SECTION Keeps-->
    <div class="masonry-container-profile">
        <div class="keep-container" v-for="k in keeps" :key="k.id">
            <!-- Keep Masonry -->
            <div class="p-2">
                <img @click="openKeepDetails(k)" :src="k.img" alt="keep image" class="img-fluid img-custom"
                    :title="'Open ' + k.name + ' details'" />
                <div class="d-flex justify-content-between" style="height: 0px">
                    <h5 class="keep-name">
                        {{ k.name }}
                    </h5>
                </div>
            </div>
        </div>
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

export default {
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
                if (v.isPrivate && v.creatorId != userId) {
                    router.push({ name: 'Home' })
                    Pop.toast("You do not have access to this vault. 🔒")
                    return
                }
                AppState.activeVault = v
                router.push({ name: 'Vault', params: { id: v.id } })
            },

            createVault() {
                Modal.getOrCreateInstance(document.getElementById("new-vault-modal")).show()
            },
            createKeep() {
                Modal.getOrCreateInstance(document.getElementById('new-keep-modal')).show()
            }
        };

    }
}
</script>

<style>
.bg-img {
    height: 20vh;
}

.keep-name {
    transform: translateY(-1.3em);
    margin-left: 0.8em;
    color: whitesmoke;
    text-shadow: 3px 3px 4px black;
    font-family: 'Marko One', serif;
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