<template>
  <header class="app-bg">
    <Banner />
    <!-- <Navbar /> -->
  </header>
  <main class="app-bg">
    <router-view />

    <Modal id="keepForm" modal-title="Create Keep">
      <KeepForm />
    </Modal>

    <Modal id="vaultForm" modal-title="Create Vault">
      <VaultForm />
    </Modal>

    <Modal id="keepDetails" modal-title="">
      <KeepDetails />
    </Modal>

    <Modal id="accountForm" modal-title="Update Account">
      <AccountForm />
    </Modal>

  </main>
  <footer class="app-bg">
    Made with ✨💀🌙 by Sautrah
  </footer>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Banner from './components/Banner.vue'
import KeepForm from './components/KeepForm.vue'
import VaultForm from './components/VaultForm.vue'
import KeepDetails from './components/KeepDetails.vue'
import AccountForm from './components/AccountForm.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState),
      activeKeep: computed(() => AppState.activeKeep),
      component: 'modal',
    }
  },
  components: { Banner, KeepForm, VaultForm, KeepDetails, AccountForm, },
  async openKeepDetails(k) {
    AppState.activeKeep = k;
    Modal.getOrCreateInstance(document.getElementById('keepDetails')).show()
    await keepsService.incrementViews();
  },
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}

//SECTION media  

.thumbnail-img {
  border-radius: 50%;
  height: 2em;
  width: 2em;
  transform: translateY(-2.2em);
  margin-right: 0.5em;
}

.vault-card {
  height: 20vh;
  // width: 50vw;
  background-size: cover;
}

//SECTION Vault Style

.vault-name {
  transform: translateY(4.5em);
  margin-left: 0.5em;
  color: whitesmoke;
  text-shadow: 3px 3px 4px black;
  font-family: 'Marko One', serif;
}


.banner-img {
  width: 85vw;
  height: 50vh;
  object-fit: cover;
}

// .vault-img {
//   background-size: cover;
// }

.keep-container {
  padding: 0px;
  animation-name: fadeInto;
  animation-duration: 5000ms;
}

//SECTION background colors 

main {
  background-color: #FEF6F0;
}

.app-bg {
  background-color: #FEF6F0;
}

//SECTION FONTS

.keep-name {
  transform: translateY(-1.3em);
  margin-left: 0.8em;
  color: whitesmoke;
  text-shadow: 3px 3px 4px black;
  font-family: 'Marko One', serif;
}

.titles {
  position: absolute;
  left: 32.85%;
  right: 65.62%;
  top: 63.61%;
  bottom: 34.98%;

  background: #FFFFFF;
  transform: matrix(-1, 0, 0, 1, 0, 0);
  font-family: 'Oxygen', sans-serif;

}

.counts {
  color: #636E72;
  font-family: 'Oxygen', sans-serif;
}

.img-font {
  position: absolute;
  left: 4.62%;
  right: 38.97%;
  top: 68.7%;
  bottom: 8.4%;

  font-family: 'Quando', serif;
  font-style: normal;
  font-weight: 400;
  font-size: 24px;
  line-height: 30px;
  /* identical to box height */


  color: #FFFFFF;

  text-shadow: 1px 1px 4px rgba(0, 0, 0, 0.25);
}

.icon {
  position: absolute;
  left: 32.85%;
  right: 65.62%;
  top: 63.61%;
  bottom: 34.98%;

  background: #FFFFFF;
  transform: matrix(-1, 0, 0, 1, 0, 0);
}

footer {
  display: grid;
  place-content: center;
  height: 32px;
}

.masonry-container {
  column-count: 4;
  column-gap: 3em;
}

@media only screen and (max-width:1068px) {
  .masonry-container {
    column-count: 3;
    column-gap: 3em;
  }

  @media only screen and (max-width:768px) {
    .masonry-container {
      column-count: 2;
      column-gap: 3em;
    }
  }

  @media only screen and (max-width:538px) {
    .masonry-container {
      column-count: 1;
      column-gap: 3em;
    }
  }
}
</style>
