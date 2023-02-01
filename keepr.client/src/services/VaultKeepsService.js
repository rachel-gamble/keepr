import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class VaultKeepsService {
    async addKeepToVault(vault) {
        const vaultKeep = {}
        vaultKeep.vaultId = vault.id
        vaultKeep.keepId = AppState.activeKeep.id
        const res = await api.post('api/vaultkeeps', vaultKeep)
        const modifiedKeep = AppState.activeKeep
        modifiedKeep.kept++
        await api.put("api/keeps/" + modifiedKeep.id, modifiedKeep)
        logger.log('keep added to vault')
    }

    async getKeepsByVaultId(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}/keeps`)
        AppState.vaultKeeps = res.data
    }

    async removeFromVault() {
        const vaultKeepId = AppState.activeKeep.vaultKeepId
        await api.delete("api/vaultkeeps/" + vaultKeepId)
        const index = AppState.vaultKeeps.find(vk => vk.id == vaultKeepId)
        // adds keep to the top of the vault, yay figured the splice out!
        AppState.vaultKeeps.splice(index, 1)
    }

}
export const vaultKeepsService = new VaultKeepsService();