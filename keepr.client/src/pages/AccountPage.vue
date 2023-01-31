<template>
  <div class="container-fluid app-bg">
    <section class="row">

      <img :src="account.coverImg" class="img-fluid">
      <div class="about text-center">
        <img :src="account.picture" alt="" class="rounded-circle prof-pic" />
        <h1 class="sub-title m-2">{{ account.name }}</h1>
      </div>
    </section>

    <section class="row d-flex justify-content-center">
      <div class="col-8 justify-items-center">
        <h3 class="sub-title">Vaults</h3><br>
      </div>
      <div class="col-8 justify-items-center">
        <h3 class="sub-title">Keeps</h3>
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
      account: computed(() => AppState.account)
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

.prof-pic {
  height: 20vh;
  width: 20vh;
}
</style>
