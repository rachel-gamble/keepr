<template>
  <div class="masonry-container app-bg">
    <!--SECTION Keep Img-->
    <section class="row">
      <div v-for="k in keeps" :key="k.id" class="card my-3 elevation-5 hover-card keep-container mx-2"
        style="min-height: 20vh">
        <img @click="openKeepDetails(k)" :src="k?.img ? k.img : 'https://thiscatdoesnotexist'" alt="keep image"
          class="img-fluid hover-shadow rounded img-custom" :title="'Open ' + k.name + ' details'">
        <!--SECTION Keep Name + Creator-->
        <div class="d-flex justify-content-between mobile-cleanup align-items-center" style="height: 0px">
          <h4 class="keep-name">
            {{ k.name }}
          </h4>
          <img @click="goToProfile(k.creator.id)" :src="k.creator.picture" alt="profile image"
            class="thumbnail-img selectable rounded-circle" :title="'Posted by' + k.creator.name" />
        </div>
      </div>
    </section>

  </div>
</template>

<script>
import { onMounted, computed, ref } from 'vue';
import { AppState } from '../AppState';
import { useRouter } from 'vue-router';
import { keepsService } from '../services/KeepsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import KeepForm from '../components/KeepForm.vue';
import VaultForm from '../components/VaultForm.vue';
import KeepDetails from '../components/KeepDetails.vue';
import { Modal } from 'bootstrap';

export default {
  setup(props) {
    const router = useRouter();
    //TODO - Add watch effect for new keeps
    // watchEffect(() => {
    //         if (AppState.keeps) {
    //         }
    //     })
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
      component: 'modal',

      async openKeepDetails(k) {
        AppState.activeKeep = k;
        Modal.getOrCreateInstance(document.getElementById('keepDetails')).show()
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
  },
  components: { KeepForm, VaultForm, KeepDetails, },
}
</script>

<style scoped lang="scss">
.keep-container {
  padding: 0px;
  animation-name: fadeInto;
  animation-duration: 5000ms;
}

.keep-name {
  transform: translateY(-1.3em);
  margin-left: 0.8em;
  color: whitesmoke;
  text-shadow: 3px 3px 4px black;
  font-family: 'Marko One', serif;
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
