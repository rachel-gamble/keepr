<template>
    <div class="container-fluid">
        <section class="row">
            <div class="masonry-container">
                <div class="keep-container" v-for="k in vaultKeeps" :key="k.id">
                    <!--TODO add on click-->
                    <img @click="openVaultKeepModal(k)" :src="k.img" class="" title="Open Details">
                    <div>
                        <div class="keep-name fs-2">{{ k.name }}</div>
                    </div>
                </div>
                <div>
                    <button @click="removeVault()" class="btn btn-dark">Delete Vault</button>
                    <!-- <div class="counts">{{ vault }}</div> -->
                </div>
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
            }
        };

    }
}
</script>

<style>

</style>