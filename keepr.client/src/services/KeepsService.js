class KeepsService {
    async create(formData){
        const res = await api.post("api/keeps", formData)
        AppState.keeps.unshift(res.data)
    }
}

export const keepsService = new KeepsService()