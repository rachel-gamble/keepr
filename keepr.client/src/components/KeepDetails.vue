<template>
    <div class="container-fluid app-bg">
        <section class="row app-bg">
            <!--SECTION LEFT SIDE-->
            <div class="col-md-6 p-0">
                <img :src="activeKeep?.img" alt="" class="img-fluid rounded-top selectable hover-shadow"
                    title="keep image">
            </div>

            <!--SECTION Right SIDE-->

            <div class="col-md-6 align-items-center">

                <!-- view count -->
                <div class="d-flex counts m-2 justify-content-center">
                    <i class="mdi mdi-eye mx-1 bold"></i>
                    <p class=""> {{ activeKeep?.views }} </p>
                    <p class="mx-2">|</p>
                    <!-- keep count-->
                    <p class="keep-box px-1"> K </p>
                    <p class="mx-1">{{ activeKeep?.kept }}</p>
                </div>

                <!--body-->
                <!--title + description-->
                <section class="align-content-center">
                    <div class="text-center keep-title">
                        <h2>{{ activeKeep?.name }}</h2>
                    </div>
                    <div class="keep-body">
                        {{ activeKeep?.description }}
                    </div>
                </section>

                <!--bottom-->
                <section class="justify-space-between m-2">

                    <!-- add keep to vault-->
                    <div>

                        <form class="dropdown" @submit.prevent="addKeepToVault()">
                            <label class="p-2" for="add-to-vault-select"><small>Add To Vault:</small></label>
                            <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton"
                                data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                🌱
                            </button>
                            <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                <li v-for="mv in myVaults" :key="mv" value="mv" class="action"><a class="dropdown-item"
                                        href="#" @click.prevent="addKeepToVault()">
                                        {{ mv?.name.substring(0, 15) }}
                                    </a></li>
                            </div>
                            <button type="submit" class="btn vault-btn selectable mdi mdi-check-outline"
                                title="Submit"></button>
                        </form>
                    </div>
                </section>
                <!-- profile info -->
                <div class="mt-1">
                    <img :src="activeKeep?.creator.picture" alt=""
                        class="img-fluid prof-img rounded-circle app-bg selectable"
                        :title="`Created by ${activeKeep?.creator.name}`" @click="goToProfile()" />
                    {{ activeKeep?.creator.name }}
                </div>
            </div>
        </section>
    </div>
</template>

<script>
import { useRouter } from 'vue-router';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger'
import { ref, computed } from 'vue';
import { Modal } from 'bootstrap';
import { keepsService } from '../services/KeepsService';
import { vaultKeepsService } from '../services/VaultKeepsService';
// import { Auth0ConfigurationOptions } from '@bcwdev/auth0provider-client';



export default {

    setup(props) {
        const vaultSelect = ref({})
        const router = useRouter();
        return {
            vaultSelect,
            router,
            myVaults: computed(() => AppState.myVaults),
            activeKeep: computed(() => AppState.activeKeep),
            account: computed(() => AppState.account),

            async addKeepToVault() {
                try {
                    await vaultKeepsService.addKeepToVault(vaultSelect.value)
                    Pop.toast("Keep added to your vault ✨", 'success')
                } catch (error) {
                    logger.error(error)
                    Pop.toast(error.message, 'error')
                }
            },

            async removeKeep() {
                try {
                    if (await Pop.confirm("Remove this keep?")) {
                        await keepsService.removeKeep()
                        Pop.toast("Keep removed. ✅")
                        Modal.getOrCreateInstance(document.getElementById("#keepModal")).hide()
                    }
                } catch (error) {
                    logger.error(error)
                    Pop.toast(error.message, 'error')
                }
            },

            async goToProfile() {
                try {
                    const profileId = AppState.activeKeep.creatorId
                    router.push({ name: 'Profile', params: { id: profileId } })
                    Modal.getOrCreateInstance(document.getElementById("keepDetails")).hide()
                } catch (error) {
                    logger.error(error)
                    Pop.toast(error.message, 'error')
                }
            }
        }


    }
}
</script>

<style>
.prof-img {
    border-radius: 50%;
    height: 5em;
    width: 5em;
}

.keep-box {
    font-family: 'Marko One', serif;
    box-sizing: border-box;
    border: 1.66787px solid #2D2D2D;
    border-radius: 5px;
}

.counts {
    color: #636E72
}

/* .left-section {
  display: flex;
}

.right-section {
  display: flex;
} */

.vault-btn {
    background-color: #A76BBD;
    color: #FEF6F0;
}

.keep-title {
    position: absolute;
    width: 430px;
    height: 61px;
    left: 796px;
    top: 362px;

    font-family: 'Marko One';
    font-style: normal;
    font-weight: 400;
    font-size: 48px;
    line-height: 64px;
    text-align: center;

    /* Dracula Orchid */

    color: #2D3436;
}

.keep-body {
    position: absolute;
    width: 427px;
    height: 152px;
    left: 800px;
    top: 437px;

    font-family: 'Inter', sans-serif;
    font-style: normal;
    font-weight: 400;
    font-size: 18px;
    line-height: 175%;
    /* or 32px */


    /* American River */

    color: #636E72;
}

.save-btn {
    position: absolute;
    width: 88px;
    height: 37px;
    left: 934px;
    top: 754px;

    font-family: 'Oxygen';
    font-style: normal;
    font-weight: 700;
    font-size: 20px;
    line-height: 25px;
    display: flex;
    align-items: center;
    text-align: center;

    color: #FEF6F0;
}

.views {
    position: absolute;
    left: 65.9%;
    right: 32.57%;
    top: 19.82%;
    bottom: 78.71%;

    /* American River */

    background: #636E72;
}

.view-count {
    /* 322 */
    position: absolute;
    width: 24px;
    height: 16px;
    left: 980px;
    top: 203px;

    font-family: 'Sansation';
    font-style: normal;
    font-weight: 400;
    font-size: 14px;
    line-height: 16px;
    /* identical to box height */


    /* American River */

    color: #636E72;
}

.keep-icon {
    /* k */


    position: absolute;
    width: 12px;
    height: 15px;
    left: 1032px;
    top: 202px;

    font-family: 'Marko One';
    font-style: normal;
    font-weight: 400;
    font-size: 14px;
    line-height: 19px;
    text-align: center;

    /* American River */

    color: #636E72;
}

.keep-border {
    /* Rectangle 2 */


    position: absolute;
    width: 20px;
    height: 20px;
    left: 1028px;
    top: 201px;

    /* American River */

    border: 1px solid #636E72;
    border-radius: 5px;
}

.keep-count {
    /* 75 */


    position: absolute;
    width: 16px;
    height: 16px;
    left: 1057px;
    top: 203px;

    font-family: 'Sansation';
    font-style: normal;
    font-weight: 400;
    font-size: 14px;
    line-height: 16px;
    /* identical to box height */


    /* American River */

    color: #636E72;
}
</style>