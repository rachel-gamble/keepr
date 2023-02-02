<template>
    <div class="modal-body">
        <form @submit.prevent="createKeep()">

            <div class="form-floating mb-3">
                <input v-model="editable.name" required type="text" class="form-control" id="keepName"
                    placeholder="Title..." maxlength="15">
                <label for="keepName">Title</label>
            </div>

            <div class="form-floating mb-3">
                <input v-model="editable.img" required type="url" class="form-control" id="keepImg"
                    placeholder="Image URL...">
                <label for="keepImg">Image URL...</label>
            </div>

            <div class="form-floating">
                <textarea v-model="editable.description" required type="text" class="form-control" id="keepDescription"
                    placeholder="Keep Description...">
                </textarea>
                <label for="keepName" maxlength="25">Keep Description</label>
            </div>

            <div class="modal-footer">
                <button type="submit" class="btn btn-dark" title="Create">Create</button>
            </div>

        </form>
    </div>
</template>

<script>
import { ref } from 'vue';
import { keepsService } from '../services/KeepsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { Modal } from 'bootstrap';

export default {
    setup(props) {
        const editable = ref({})
        return {
            editable,
            showModal: false,
            async createKeep() {
                try {
                    await keepsService.createKeep(editable.value)
                    Modal.getOrCreateInstance(document.getElementById('keepDetails')).hide()
                    this.showModal = false;
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