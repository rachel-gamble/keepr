<template>
    <form class="input-group p-3 justify-content-center" @submit.prevent="updateAccount()">
        <div class="d-flex flex-column">
            <label class="mt-2" for="name">Change username</label>
            <input class="mt-2 col-12 form-input" type="text" name="name" placeholder="new username..."
                v-model="editing.name" />

            <label class="mt-2" for="picture">Profile Picture:
            </label>
            <input class="mt-2 form-input" type="url" name="picture" placeholder="new profile picture url..."
                v-model="editing.picture" />

            <label class="mt-2" for="coverImg">Cover Image:
            </label>
            <input class="mt-2 form-input" type="url" name="picture" placeholder="New cover img url..."
                v-model="editing.picture" />

        </div>
        <div class="mt-2">
            <button type="submit" class="btn selectable mt-2 hover elevation-2 submit">Submit</button>
        </div>
    </form>
</template>

<script>
import { computed, ref, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { accountService } from '../services/AccountService.js'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop.js'
import { Modal } from 'bootstrap';

export default {
    setup() {
        const editing = ref({})
        watchEffect(() => {
            if (AppState.account.id) {
                editing.value = { ...AppState.account }
            }
        })
        return {
            editing,
            account: computed(() => AppState.account),
            async updateAccount() {
                try {
                    await accountService.updateAccount(editing.value);
                    Modal.getOrCreateInstance(document.getElementById('accountForm')).hide()
                    Pop.toast("Profile updated. 🌱", "success")
                } catch (error) {
                    Pop.toast("error", "error")
                    logger.log(error)
                }
            },
        }
    }
}

</script>

<style>

</style>