<template>
    <div class="app-bg">
        <!--SECTION LEFT SIDE-->
        <section class="row">
            <div class="col-md-6 col-sm-12 order-md-1 order-sm-1 app-bg left-side d-flex p-0">
                <img :src="activeKeep?.img" alt="" class="img-fluid rounded-top hover-shadow app-bg" title="">
            </div>


            <!--SECTION Right SIDE-->
            <div class="col-md-6 order-md-2 order-sm-2 d-flex flex-column justify-content-between">
                <section class="row p-0 my-2 counts">
                    <div class="d-flex justify-content-center align-content-center">
                        <i class="mdi mdi-eye mx-1"></i>
                        <p class=""> {{ activeKeep?.views }} </p>
                        <p class="mx-2">|</p>
                        <p class="keep-box px-1"> K </p>
                        <p class="mx-1">{{ activeKeep?.kept }}</p>
                    </div>
                </section>


                <section class="row">
                    <div class="justify-content-center">
                        <div class="text-center keep-title">
                            <h2 class="mx-1">{{ activeKeep?.name }}</h2>
                        </div>
                        <div class="mx-2 keep-body text-start">
                            {{ activeKeep?.description }}
                        </div>
                    </div>
                </section>


                <section class="row oxygen">
                    <div class="d-flex justify-content-center align-items-center">
                        <div class="col-6 my-2">
                            <span v-if="activeKeep?.creatorId == account.id"
                                class="mdi mdi-trash-can selectable hover text-center" @click.prevent="removeKeep()"
                                title="delete keep"></span>
                            <form class="dropdown" @submit.prevent="addKeepToVault()">
                                <label class="ms-2" for="add-to-vault-select"><small>Add To Vault:</small></label>
                                <select name="" v-model="vaultSelect" class="action selectable m-1" title="Add to vault"
                                    required>
                                    <option v-for="mv in myVaults" :key="mv" :value="mv" class="action"
                                        :title="mv.name">
                                        {{ mv?.name.substring(0, 15) }}
                                    </option>
                                </select>
                                <button type="submit" class="btn vault-btn selectable fs-6 my-1 mx-1"
                                    title="save keep">save</button>
                            </form>
                        </div>

                        <div class="col-6 d-flex justify-content-end">
                            <img :src="activeKeep?.creator.picture" alt=""
                                class="img-fluid prof-img rounded-circle app-bg selectable"
                                :title="`Created by ${activeKeep?.creator.name}`" @click="goToProfile()" />
                            <div class="fs-6 oxygen c-name mx-2">
                                {{ activeKeep?.creator.name }}
                            </div>
                        </div>
                    </div>
                </section>
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
input {
    border: #A76BBD;
}

@media only screen and (max-width:768px) {
    /* .middle {
        position: relative;
        top: 0%;
        left: 0%;
        text-overflow: wrap;
        transform: translate(0%, 0%);

    } */

    /* .modal-size {
        height: 70vh;
    }

    .keep-img {
        height: 60vh;
    } */
}

@media only screen and (max-width:538px) {
    /* 
    .keep-img {
        height: 50vh;
    } */
    /* 
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
    } */
}

/* .keep-img {
    height: 60vh !important;
} */
/* 
.prof-img {
    border-radius: 50%;
    height: 3em;
    width: 3em;
    object-fit: cover;
}

.keep-title {
    font-family: 'Marko One', serif;
}

*/
.keep-body {
    font-family: 'Sansation';
    word-wrap: break-word;
    white-space: normal;
    text-overflow: wrap;
}

.san-font {
    font-family: 'Sansation';
}

/* .keep-box {
    font-family: 'Marko One', serif;
    box-sizing: border-box;
    border: 1.66787px solid #636E72;
    border-radius: 5px;
} */

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

/* 
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
    background: #636E72;
}

.view-count {
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
    color: #636E72;
} */

/* .keep-icon {
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
    color: #636E72;
} */

/* .keep-border {
    position: absolute;
    width: 20px;
    height: 20px;
    left: 1028px;
    top: 201px;
    border: 1px solid #636E72;
    border-radius: 5px;
} */

/* .keep-count {
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
    color: #636E72;
} */
</style>