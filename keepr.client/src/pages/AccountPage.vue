<template>
  <div class="container-fluid app-bg">
    <section class="row justify-content-center">

      <img :src="account.coverImg" class="img-fluid banner-img">
      <div class="about text-center">
        <img :src="account.picture" alt="" class="profile-icon" />
        <!--counters-->
        <div class="flex-column">
          <h1>{{ account?.name }}</h1>
          <div class="counts">
            <span class="bold">{{ numberOfVaults }} Vaults</span>
            <span class="mx-1">|</span>
            <span class="bold">Keeps: {{ numberOfKeeps }}</span>
          </div>
        </div>
      </div>
    </section>
    <!--SECTION Vaults-->
    <section class="row d-flex justify-content-center">
      <h3 class="sub-title fw-bold mx-3 my-1">Vaults</h3>
      <!--v-for-->
      <div v-for="vault in vaults" :key="vault.id" class="col-lg-3 col-md-6 col-sm-12">
        <div class="" @click="goToVault(vault)">
          <div
            class="rounded my-2 mb-2 elevation-5 hover-card keep-container vault-card image-custom image-fluid selectable hover-shadow"
            :style="`background-image: url(${vault?.img})`"
            :title="'Go to ' + vault?.name + ' by ' + vault?.creator.name">
            <div class="d-flex justify-content-between-mobile-cleanup">
              <div v-if="vault.isPrivate" class="mdi mdi-lock text-white m-2 elevation-5" title="is private"></div>

              <h4 class="vault-name">
                {{ vault?.name }}
              </h4>
            </div>
            <br><br>
          </div>
        </div>

        <!--end v-for-->
      </div>
    </section>

    <section class="row d-flex justify-content-center">
      <h3 class="sub-title fw-bold mx-3 my-1 mb-3">Keeps</h3>
      <!--v-for--> <!-- Keep Masonry -->
      <div v-for="k in keeps" :key="k.id" class="col-lg-3 col-md-6 col-sm-12">
        <div class="" @click="openKeepDetails(k)">
          <div
            class="rounded my-2 mb-2 elevation-5 hover-card keep-container vault-card image-custom image-fluid selectable hover-shadow"
            :style="`background-image: url(${k?.img})`" :title="'Open ' + k?.name + ' by ' + k?.creator.name">
            <div class="d-flex justify-content-between-mobile-cleanup">
              <h4 class="vault-name">
                {{ k?.name }}
              </h4>
            </div>
            <br><br>
          </div>
        </div>
        <!--end v-for-->
      </div>
    </section>
  </div>
</template>

<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { watchEffect } from '@vue/runtime-core'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { profilesService } from '../services/ProfilesService'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { useRoute, useRouter } from 'vue-router'
import { Modal } from 'bootstrap'
import KeepDetails from '../components/KeepDetails.vue'
export default {
  props: {
    vault: { type: Object, required: true },
    keep: { type: Object, required: true },
  },
  setup(props) {
    const route = useRoute();
    const router = useRouter();
    watchEffect(async () => {
      try {
        if (route.params.id && route.name == "Account") {
          await profilesService.getUserProfile(route.params.id);
          await keepsService.getUserKeeps(route.params.id);
          await vaultsService.getUserVaults(route.params.id);
        }
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      route,
      account: computed(() => AppState.account),
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.userVaults),
      numberOfKeeps: computed(() => AppState.keeps.length),
      numberOfVaults: computed(() => AppState.userVaults.length),
      async openKeepModal(k) {
        AppState.activeKeep = k;
        Modal.getOrCreateInstance(document.getElementById('keep-modal')).show()
        await keepsService.incrementViews();
      },

      async goToVault(v) {
        const userId = AppState.account.id
        console.log(userId, v.creatorId)
        if (v.creatorId != userId && v.isPrivate == true) {
          router.push({ name: 'Home' });
          Pop.toast("You do not have access to this vault. 🔒")
          return
        }
        AppState.activeVault = v
        router.push({ name: 'Vault', params: { id: v.id } })
      },

      async openKeepDetails(k) {
        AppState.activeKeep = k;
        Modal.getOrCreateInstance(document.getElementById('keepDetails')).show()
        await keepsService.incrementViews();
      },

      createVault() {
        Modal.getOrCreateInstance(document.getElementById('new-vault-modal')).show()
      },
      createKeep() {
        Modal.getOrCreateInstance(document.getElementById('new-keep-modal')).show()
      }
    };

  },
  components: { KeepDetails, },
}
</script>

<style scoped>
/* img {
  max-width: 100px;
} */

.sub-title {
  font-family: 'Oxygen', sans-serif;
}

.keep-name {
  transform: translateY(-1.3em);
  margin-left: 0.8em;
  color: whitesmoke;
  text-shadow: 3px 3px 4px black;
  font-family: 'Marko One', serif;
}

.prof-pic {
  height: 20vh;
  width: 20vh;
}
</style>
