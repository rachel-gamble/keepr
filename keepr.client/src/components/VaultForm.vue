<template>
    <div class="modal-body">
        <form @submit.prevent="createVault()">

            <div class="form-floating mb-3">
                <input v-model="editable.name" required type="text" class="form-control" id="keepName"
                    placeholder="Title...">
                <label for="keepName">Title</label>
            </div>

            <div class="form-floating mb-3">
                <input v-model="editable.img" required type="url" class="form-control" id="keepImg"
                    placeholder="Image URL...">
                <label for="keepImg">Image URL...</label>
            </div>

            <div class="form-check">
                <label for="defaultCheck1" class="fs-6 fw-light">Private Vaults can only be seen by you</label>
                <input v-model="editable.isPrivate" class="form-check-input" type="checkbox" value="" id="isPrivate">
                <label class="form-check-label" for="defaultCheck1">
                    Make Vault Private?
                </label>
            </div>

            <div class="modal-footer">
                <button type="submit btn-dark" class="btn">Create</button>
            </div>

        </form>
    </div>
</template>

<script>
import { ref } from 'vue';
import { vaultsService } from '../services/VaultsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';

export default {
    setup(props) {
        const editable = ref({})
        return {
            editable,
            async createVault() {
                try {
                    await vaultsService.createVault(editable.value)
                } catch (error) {
                    logger.error(error)
                    Pop.error(error.message)
                }
            }
        };

    }
}
</script>

<style>

</style>