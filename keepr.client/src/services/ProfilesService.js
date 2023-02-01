import { AppState } from "../AppState"
import { api } from "./AxiosService"

class ProfilesService {
    async getUserProfile(profileId) {
        const res = await api.get("api/profiles/" + profileId)
        AppState.activeProfile = res.data
    }
}
export const profilesService = new ProfilesService()