<template>
    <div class="container-fluid app-bg oxygen">
        <!--SECTION Banner + Vault Details-->
        <section class="row d-flex justify-content-center text-center">
            <img :src="activeVault?.img" class="banner-img" alt="profile cover image" title="Profile cover image">
            <img :src="activeVault?.creator.picture" alt="profile picture" class="profile-icon-vault">
            <h1>{{ activeVault?.creator.name }}</h1>
            <div class="flex-column">




                <!--SECTION Button-->
                <button v-show="activeVault?.creator.id == account.id" @click="removeVault()"
                    class="btn btn-dark">Delete
                    Vault</button>

                <div class="counts">
                    <span class="bold">{{ numberOfKeeps }} *11* Keeps</span>
                </div>
            </div>
        </section>
        <!--SECTION Keeps in Vault-->
        <section class="row d-flex justify-content-center">
            <div class="col-10">
                <h3 class="sub-title fw-bold mx-3 my-1 mb-3">Keeps</h3>
                <!--v-for--> <!-- Keep Masonry -->
                <div class="masonry-container">
                    <div v-for="k in vaultKeeps" :key="k.id">
                        <div class="" @click="openKeepDetails(k)">
                            <div class="rounded my-2 mb-2 elevation-5 hover-card keep-container vault-card image-custom image-fluid selectable hover-shadow"
                                :style="`background-image: url(${k?.img})`"
                                :title="'Open ' + k?.name + ' by ' + k?.creator.name">
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
import { onMounted, computed } from 'vue';
import { vaultKeepsService } from '../services/VaultKeepsService';
import Pop from '../utils/Pop';
import { useRoute, useRouter } from 'vue-router';
import { vaultsService } from '../services/VaultsService';
import { logger } from '../utils/Logger';
import { AppState } from '../AppState';
import { Modal } from 'bootstrap';
import { keepsService } from '../services/KeepsService';


export default {
    setup() {
        const route = useRoute();
        const router = useRouter();

        onMounted(async () => {
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
            numberOfKeeps: computed(() => AppState.numberOfKeeps),

            async openVaultKeepModal(k) {
                AppState.activeKeep = k;
                Modal.getOrCreateInstance(document.getElementById('vaultKeepModal')).show()
                await keepsService.incrementViews();
            },
            async removeVault() {
                try {
                    if (await Pop.confirm("Delete this Vault?", 'warning')) {
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
        };

    }
}
</script>

<style>

</style>