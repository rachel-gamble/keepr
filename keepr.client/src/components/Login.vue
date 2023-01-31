<template>
  <span class="navbar-text">
    <button class="btn selectable text-success lighten-30 text-uppercase my-2 my-lg-0" @click="login"
      v-if="!user.isAuthenticated">
      Login
    </button>
    <div v-else>
      <div class="dropdown dropstart my-2 my-lg-0">
        <div type="button" class="app-bg border-0 selectable no-select" data-bs-toggle="dropdown" aria-expanded="false">
          <div v-if="account.picture || user.picture">
            <img :src="account.picture || user.picture" alt="account photo" height="40" class="rounded-circle app-bg" />
          </div>
        </div>
        <div class="dropdown-menu dropdown-menu-lg-left p-0" aria-labelledby="authDropdown">
          <div class="list-group">
            <router-link :to="{ name: 'Account' }">
              <div class="list-group-item dropdown-item list-group-item-action" title="Go to my Profile.">
                My Profile
              </div>
            </router-link>
            <a data-bs-toggle="modal" data-bs-target="#accountForm" class="dropdown-item" @click="showModal"
              title="Edit Account">Edit
              Account</a>
            <div class="list-group-item dropdown-item list-group-item-action text-danger selectable" @click="logout"
              title="Logout">
              <i class="mdi mdi-logout"></i>
              logout
            </div>
          </div>
        </div>
      </div>
    </div>
  </span>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import { AuthService } from '../services/AuthService'
import Modal from './Modal.vue'
import AccountForm from './AccountForm.vue'


export default {
  setup() {
    return {
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      }
    }
  },
  components: { AccountForm, Modal, }
}
</script>

<style lang="scss" scoped>
img {
  width: 10vh;
  height: 10vh;
}
</style>
