<template>
  <div class="masonry-container">
    <!--SECTION Keep Card-->
    <section class="row">
      <!--NOTE k in keeps uses k.id | keep in keeps uses keep.id-->
      <div v-for="keep in keeps" :key="keep.id" class="card my-3 elevation-5 hover-card">
        <!-- <router-link :to"{name:'Vault', params: {id: vault.id}}"></router-link> -->
        <img :src="keep?.img" alt="http://thiscatdoesnotexist.com" class="img-fluid hover-shadow rounded"
          @click="openKeepDetails(keep)" :title="'Open ' + keep.name + ' details'">
        {{ keep?.name }}

      </div>
    </section>

  </div>
</template>

<script>
import { onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { computed } from 'vue';

export default {
  setup() {
    const router = useRouter;
    onMounted(async () => {
      try {
        await keepsService.getAllKeeps();
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">
// .keep-container {
//   padding: 1px;
//   animation-name: fadeInto;
//   animation-duration: 5000ms;
// }

.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
