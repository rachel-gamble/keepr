<template>
    <div class="" @click="goToVault(vault)">
        <div class="rounded vault-img m-2" :style="`background-image: url(${vault?.img})`">
            <h4 class="keep-name">
                {{ vault?.name }}
                sldkfjsaljdf
            </h4>
            <br><br>
        </div>
    </div>
</template>

<script>
import { AppState } from '../AppState';
import { router } from '../router';
export default {
    props: {
        vault: { type: Object, required: true },
    },
    setup(props) {

        return {
            async goToVault(vault) {
                const userId = AppState.account.id
                if (vault.isPrivate && vault.creatorId != userId) {
                    router.push({ name: 'Home' })
                    Pop.toast("You do not have access to this vault. 🔒")
                    return
                }
                console.log("The props id", props.vault.id)
                AppState.activeVault = vault
                router.push({ name: 'Vault', params: { id: vault.id } })
            },
        };

    }
}
</script>

<style>
.vault-img {
    height: 40vh;
}
</style>