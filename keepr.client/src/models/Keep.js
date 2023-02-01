export class Keep {
    constructor(data) {
        this.id = data.id
        this.creatorId = data.creatorId
        this.name = data.name
        this.description = data.description
        this.img = data.img || "https://images.unsplash.com/photo-1610397648930-477b8c7f0943?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Mnx8b3JjaGlkfGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=600&q=60"
        // this.img = data.img || 'https://images.unsplash.com/photo-1610397648930-477b8c7f0943?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Mnx8b3JjaGlkfGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=600&q=60'
        this.views = data.views
        this.kept = data.kept
        this.creator = data.creator
        this.vaultKeepId = data.vaultKeepId
    }
}