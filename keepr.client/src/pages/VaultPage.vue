<template>
    <div class="container-fluid app-bg oxygen">
        <!--SECTION Banner + Vault Details-->
        <section class="row d-flex justify-content-center text-center">
            <div class="">
                <img :src="activeVault?.img" class="banner-img justify-item-center text-center"
                    alt="profile cover image" title="Cover image"><br />
            </div>
            <span class="fs-1 vault-page-name">{{ activeVault?.name }}</span>
            <span class="fs-6 vault-page-name">by</span>
            <span class="fs-5 vault-page-name">{{ activeVault?.creator.name }}</span>
            <div class="up">
                <span></span>
                <!--SECTION Button-->
                <button v-show="activeVault?.creator.id == account.id" @click="removeVault()" class="btn btn-dark"
                    title="delete vault">Delete
                    Vault</button>

                <div class="counts">
                    <span class="bold">{{ vaultKeeps.length }} Keeps</span>
                </div>
            </div>
        </section>

        <!--SECTION Keeps in Vault-->
        <section class="justify-content-center up">
            <h3 class="sub-title fw-bold">Keeps</h3>
            <!--v-for-->
            <div class="row">
                <div v-for="k in vaultKeeps" :key="k.id" class="col-lg-3 col-md-6 col-sm-12">
                    <div class="">
                        <div @click="openKeepDetails(k)"
                            class="rounded my-2 mb-2 elevation-5 hover-card keep-container vault-card image-custom image-fluid selectable hover-shadow mm"
                            :style="`background-image: url(${k?.img})`"
                            :title="'Open ' + k?.name + ' by ' + k?.creator.name">
                            <div class="d-flex justify-content-between-mobile-cleanup">

                                <div v-if="account?.id == activeVault?.creatorId"
                                    class="mdi mdi-trash-can text-white m-2 hover selectable elevation-5"
                                    @click="removeFromVault(k)" title="remove keep"></div>
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
        </section>

    </div>
</template>

<script>
import { computed, watchEffect } from 'vue';
import { vaultKeepsService } from '../services/VaultKeepsService';
import Pop from '../utils/Pop';
import { useRoute, useRouter } from 'vue-router';
import { vaultsService } from '../services/VaultsService';
import { logger } from '../utils/Logger';
import { AppState } from '../AppState';
import { Modal } from 'bootstrap';
import { keepsService } from '../services/KeepsService';

export default {
    setup(props) {
        const route = useRoute();
        const router = useRouter();

        watchEffect(async () => {
            try {
                await vaultsService.getById(route.params.id)
                await vaultKeepsService.getKeepsByVaultId(route.params.id)
            } catch (error) {
                router.push({ name: 'Home' })
                logger.error(error)
                Pop.toast("You cannot access this vault.", 'error')
            }
        })
        return {
            route,
            router,
            vaultKeeps: computed(() => AppState.vaultKeeps),
            keepsInVault: computed(() => AppState.vaultKeeps.length),
            activeVault: computed(() => AppState.activeVault),
            account: computed(() => AppState.account),
            keep: computed(() => AppState.keep),
            numberOfKeeps: computed(() => AppState.numberOfKeeps),
            // numberOfKeeps: computed(() => AppState.keeps.length),

            async openVaultKeepModal(k) {
                AppState.activeKeep = k;
                Modal.getOrCreateInstance(document.getElementById('vaultKeepModal')).show()
                await keepsService.incrementViews();
            },
            async removeVault() {
                try {
                    if (await Pop.confirm("Delete this Vault?", 'danger')) {
                        await vaultsService.removeVault(route.params.id)
                        router.push({ name: 'Home' })
                        Pop.toast("Vault removed.", 'success')
                    }
                } catch (error) {
                    logger.error(error)
                    Pop.toast("Error removing this vault.", 'error')
                }
            },
            async openKeepDetails(k) {
                AppState.activeKeep = k;
                Modal.getOrCreateInstance(document.getElementById('keepDetails')).show()
                await keepsService.incrementViews();
            },

            async removeFromVault(k) {
                try {
                    if (await Pop.confirm("Remove this Keep from the Vault?", 'warning')) {
                        await vaultKeepsService.removeFromVault(k)
                        Modal.getOrCreateInstance(document.getElementById('keepDetails')).hide()
                        Pop.toast("Keep removed from vault. ????", 'success')
                    }
                } catch (error) {
                    logger.error(error)
                    Pop.toast("Error removing keep.", 'error')
                }
            }
        };

    }
}
</script>

<style>
.up {
    transform: translateY(-12vh);
}

/* .mm{

} */

.vault-page-name {
    transform: translateY(-18vh);
    color: whitesmoke;
    text-shadow: 3px 3px 4px black;
    font-family: 'Marko One', serif;
}

/* .vault-page-creator {
    transform: translateY(-1vh);
    margin-left: 0.5em;
    color: whitesmoke;
    text-shadow: 3px 3px 4px black;
    font-family: 'Marko One', serif;
} */
</style>