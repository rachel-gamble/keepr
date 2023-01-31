<template>
    <Modal id="keep-details">
        <!--SECTION LEFT SIDE-->
        <div class="left-section">
            <img :src="activeKeep?.img" alt="http://thiscatdoesnotexist.com" class="img-fluid rounded-top">
        </div>

        <!--SECTION LEFT SIDE-->
        <div class="right-section">
            <!--top-->
            <section class="text center">
                <div class="views">                    <i class="mdi mdi-eye"></i> {{ keep.views }}
                </div>
                <div>
                    <p>K</p> {{ keep.kept }}
                </div>

            </section>
            <!--body-->
            <section class="align-content-center">
                <div class="text-center keep-title">
                    <h2>{{ keep.name }}</h2>
                </div>
                <div class="keep-body">
                    {{ keep.description }}
                </div>
            </section>
            <!--bottom-->
            <section class="justify-space-between m-2">
                <div v-if="account.id" class="col-md-8">
                    <!--TODO Add keep to user's vault here-->
                    <form class="dropdown">
                        <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton"
                            data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            Plants
                        </button>
                        <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                            <a class="dropdown-item" href="#"></a>
                            <a class="dropdown-item" href="#"></a>
                            <a class="dropdown-item" href="#"></a>
                        </div>
                    </form>
                </div>
                <div class="col-4 selectable">
                    <router-link class="" :to="{ name: 'Profile' }">
                        {{ keep.creator.picture }} {{ keep.creator.name }}
                    </router-link>
                </div>
            </section>

        </div>
    </Modal>

</template>

<script>

export default {



    setup() {

        return {};

    }
}
</script>

<style>
/* .left-section {
  display: flex;
}

.right-section {
  display: flex;
} */


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