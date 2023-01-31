<template>
    <!-- <Modal id="vaultForm"> -->
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
                <small for="private-boolean" class="smol">Private Vaults can only be seen by you</small><br>
                <input v-model="editable.isPrivate" class="form-check-input" type="checkbox" id="isPrivate">
                <label class="form-check-label" for="private-boolean">
                    Make Vault Private?
                </label>
            </div>

            <div class="modal-footer">
                <button type="submit" class="btn btn-dark" title="Create">Create</button>
            </div>

        </form>
    </div>
    <!-- </Modal> -->
</template>

<script>
import { ref } from 'vue';
import { vaultsService } from '../services/VaultsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { Modal } from 'bootstrap';

export default {
    setup(props) {
        const editable = ref({})
        return {
            editable,
            async createVault() {
                try {
                    await vaultsService.createVault(editable.value)
                    Modal.getOrCreateInstance(document.getElementById('vaultForm')).hide()
                    Pop.toast("Vault created. ✨", 'success')
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
.smol {
    color: rgb(79, 77, 77)
}
</style>