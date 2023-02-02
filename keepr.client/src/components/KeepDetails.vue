<template>
    <div class="container-fluid app-bg modal-size">
        <section class="row app-bg">
            <!--SECTION LEFT SIDE-->
            <div class="col-md-6 col-sm-12 order-md-1 order-sm-1 p-0 app-bg">
                <img :src="activeKeep?.img" alt="" class="img-fluid rounded-top selectable hover-shadow keep-img app-bg"
                    title="keep image">
            </div>

            <!--SECTION Right SIDE-->

            <div class="col-md-6 col-sm-12 order-md-2 order-sm-2 align-items-center right-side">

                <!-- view count -->
                <div class="row">

                    <div class="d-flex counts m-2 justify-content-center">
                        <i class="mdi mdi-eye mx-1"></i>
                        <p class=""> {{ activeKeep?.views }} </p>
                        <p class="mx-2">|</p>
                        <!-- keep count-->
                        <p class="keep-box px-1"> K </p>
                        <p class="mx-1">{{ activeKeep?.kept }}</p>
                    </div>
                    <div class="text-center">
                        <span v-show="account.id == activeKeep?.creator.id"
                            class="btn btn-light mdi mdi-trash-can app-bg" title="Delete this keep"
                            @click.prevent="removeKeep()"></span>
                    </div>
                </div>
                <!--body-->
                <!--title + description-->
                <div class="row">

                    <div class="align-items-center middle">
                        <div class="text-center keep-title">
                            <h2 class="mx-1">{{ activeKeep?.name }}</h2>
                        </div>
                        <div class="mx-2 justify-content-center keep-body ">
                            {{ activeKeep?.description }}
                        </div>
                    </div>

                </div>
                <!--bottom-->
                <div class="row justify-space-between">

                    <div class="col-md-6 oxygen bottom">
                        <!--SECTION Add to Vault Button-->
                        <div v-if="account.id" style="" class="col-md-6 col-sm-12">
                            <form class="dropdown" @submit.prevent="addKeepToVault()">
                                <label class="p-2" for="add-to-vault-select"><small>Add To Vault:</small></label><br />
                                <select name="" v-model="vaultSelect" class="action" title="Add to vault" required>
                                    <option v-for="mv in myVaults" :key="mv" :value="mv" class="action"
                                        :title="mv.name">
                                        {{ mv?.name.substring(0, 15) }}
                                    </option>
                                </select>
                                <button type="submit" class="btn vault-btn selectable fs-6 mb-2 mt-1 mx-1"
                                    title="save keep">save</button>
                            </form>
                        </div>
                    </div>
                    <!-- profile info -->
                    <div class="mt-1 my-3 bottom col-md-5 col-sm-12 text-end" style="">
                        <img :src="activeKeep?.creator.picture" alt=""
                            class="img-fluid prof-img rounded-circle app-bg selectable"
                            :title="`Created by ${activeKeep?.creator.name}`" @click="goToProfile()" />
                        <div class="fs-6 oxygen">
                            {{ activeKeep?.creator.name }}
                        </div>
                    </div>
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
                        Modal.getOrCreateInstance(document.getElementById('keepDetails')).hide()
                        Pop.toast("Keep removed. ✅")
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

<style scoped>


.middle {
    position: absolute;
    top: 55%;
    left: 75%;
    text-overflow: wrap;
    transform: translate(-50%, -50%)
}

.bottom {
    align-self: flex-end;
    text-overflow: wrap;
    position: absolute;
    bottom: 0;
}

@media only screen and (max-width:1068px) {

    .middle {
        position: relative;
        top: 0%;
        left: 0%;
        text-overflow: wrap;
        transform: translate(0%, 0%)
    }

    .bottom {
        align-self: flex-end;
        text-overflow: wrap;
        position: absolute;
        bottom: 0;
    }
}

@media only screen and (max-width:768px) {
    .middle {
        position: relative;
        top: 0%;
        left: 0%;
        text-overflow: wrap;
        transform: translate(0%, 0%)
    }

    .modal-size {
        height: 70vh;
    }

    .keep-img {
        height: 60vh;
    }
}

@media only screen and (max-width:538px) {

    .keep-img {
        height: 50px;
    }

    .middle {
        position: relative;
        top: 0%;
        left: 0%;
        overflow: hidden;
        white-space: normal;
        text-overflow: ellipsis;
        transform: translate(0%, 0%);
        height: 40vh;
    }

    .bottom {
        position: absolute;
        bottom: 0;
        white-space: normal;
        margin-top: 50vh;
    }

    .right-side {
        justify-content: space-around;
        position: absolute;
        bottom: 0;
    }
}

/* .keep-img {
    height: 60vh !important;
} */

.prof-img {
    border-radius: 50%;
    height: 3em;
    width: 3em;
    object-fit: cover;
}

.keep-title {
    font-family: 'Marko One', serif;
}

.keep-body {
    font-family: 'Sansation';
    white-space: normal;
    text-overflow: wrap;
    word-wrap: break-word
}

.keep-box {
    font-family: 'Marko One', serif;
    box-sizing: border-box;
    border: 1.66787px solid #636E72;
    border-radius: 5px;
}

.counts {
    color: #636E72;
    font-family: 'Sansation';
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