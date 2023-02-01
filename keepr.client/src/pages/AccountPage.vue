<template>
  <div class="container-fluid app-bg">
    <section class="row">

      <img :src="account.coverImg" class="img-fluid">
      <div class="about text-center">
        <img :src="account.picture" alt="" class="rounded-circle prof-pic" />
        <h1 class="sub-title m-2">{{ account.name }}</h1>

        <!--counters-->

        <div class="flex-column mt-5">
          <h1>{{ profile?.name }}</h1>
          <span>Vaults: {{ numberOfVaults }}</span>
          <span class="mx-2">|</span>
          <span class="">Keeps: {{ numberOfKeeps }}</span>
        </div>
      </div>
    </section>
    <!-- Vaults -->
    <section class="row d-flex justify-content-center">
      <div class="col-8 justify-items-center">
        <h3 class="sub-title fw-bold">Vaults</h3><br>

        <div v-for="v in vaults" :key="v.id" :message="vault">
          <VaultCard :vault="v" />
        </div>

      </div>
      <div class="col-8 justify-items-center">
        <h3 class="sub-title fw-bold">Keeps</h3><br>
        <div v-for="v in vaults" :key="v.id" class="col-sm-6 col-md-4 col-lg-2 p-2"></div>
      </div>
    </section>

  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
export default {
  setup() {
    return {
      account: computed(() => AppState.account),
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.userVaults),
      numberOfKeeps: computed(() => AppState.keeps.length),
      numberOfVaults: computed(() => AppState.userVaults.length),
    }
  }
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
