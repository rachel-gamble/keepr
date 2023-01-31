<template>
  <div class="masonry-container">
    <!--SECTION Keep Card-->
    <section class="row">
      <div v-for="keep in keeps" :key="keep.id" class="card my-3 elevation-5 hover-card keep-container mx-2"
        style="min-height: 20vh">
        <img :src="keep?.img" alt="" class="img-fluid hover-shadow rounded img-custom" @click="openKeepDetails(k)"
          :title="'Open ' + keep.name + ' details'">
        <div class="d-flex justify-content-between mobile-cleanup align-items-center" style="height: 0px">
          <h4 class="keep-name">
            {{ keep?.name }}
          </h4>
          <img @click="goToProfile(keep.creator.id)" :src="keep.creator.picture" alt=""
            class="thumbnail-img selectable rounded-circle" :title="'Posted by' + keep.creator.name" />
        </div>
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
import Modal from '../components/Modal.vue';

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
      keeps: computed(() => AppState.keeps),

      async openKeepDetails(k) {
        AppState.activeKeep = k;
        Modal.getOrDestroyInstance(document.getElementById('keep-details')).show()
        await keepsService.incrementViews();
      },

      async goToProfile(profileId) {
        try {
          router.push({ name: 'Profile', params: { id: profileId } })
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }
    }
  }
}
</script>

<style scoped lang="scss">
.keep-container {
  padding: 0px;
  animation-name: fadeInto;
  animation-duration: 5000ms;
}

.keep-name {
  transform: translateY(-1.5em);
  margin-left: 0.8em;
  color: whitesmoke;
  text-shadow: 3px 3px 4px black;
}

.thumbnail-img {
  border-radius: 50%;
  height: 2em;
  width: 2em;
  transform: translateY(-2.2em);
  margin-right: 0.5em;
}

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

@keyframes fadeInto {
  0% {
    opacity: 0;
  }

  40% {
    opacity: 0;
  }

  100% {
    opacity: 100;
  }
}

.img-custom:hover {
  transform: scale(1.005);
}
</style>
