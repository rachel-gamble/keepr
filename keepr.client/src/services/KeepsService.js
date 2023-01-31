import { AppState } from "../AppState";
import { api } from "./AxiosService";
import { logger } from "../utils/Logger.js"
import { Keep } from "../models/Keep";

class KeepsService {
    async createKeep(formData) {
        const res = await api.post("api/keeps", formData)
        logger.log('wut', res.data)
        AppState.keeps.unshift(new Keep(res.data))
    }
    async getAllKeeps() {
        const res = await api.get("api/keeps")
        AppState.keeps = res.data;
        AppState.keeps.reverse()
    }
    async getUserKeeps(profileId) {
        const res = await api.get("api/profiles/" + profileId + "/keeps")
        AppState.keeps = res.data;
        AppState.keeps.reverse()
    }

    async incrementViews() {
        const keep = AppState.activeKeep
        const id = keep.id
        keep.views++
        const res = await api.put("api/keeps/" + id, keep)
        AppState.activeKeep = res.data
        AppState.activeKeep.vaultKeepId = keep.vaultKeepId
    }

    async deleteKeep() {
        const id = AppState.activeKeep.id
        await api.delete("api/keeps/" + id)
        const index = AppState.keeps.find(k => k.id == id)
        AppState.keeps.splice(index, 1)
    }

}

export const keepsService = new KeepsService()