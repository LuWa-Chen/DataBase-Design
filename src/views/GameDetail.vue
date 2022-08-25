<!--张宇 1952168-->
<template>
    <div class="canvas">
        <div style="height: 50px"></div>
        <GameCarousel :game_id="this.game_id"></GameCarousel>
        <div style="height: 50px"></div>
        <GameInfo :game_id="this.game_id"></GameInfo>
        <div style="height: 50px"></div>
        <GameRecommended :game_id="this.game_id"></GameRecommended>
        <div style="height: 50px"></div>
        <GameComments :game_id="this.game_id" :user_id="this.$store.state.userID"></GameComments>
        <div style="height: 50px"></div>
    </div>
</template>

<script>
import GameCarousel from "@/components/GameCarousel";
import GameInfo from "@/components/GameInfo";
import GameRecommended from "@/components/GameRecommended";
import GameComments from "@/components/GameComments";
export default {
    name: "GameDetail",
    props:['game_id'],
    components: {
        GameCarousel,
        GameInfo,
        GameRecommended,
        GameComments,
    },
}

</script>

<style scoped>
.canvas {
    position:fixed;
    width:100%;
    height:100vh;
    /*background: url("../assets/imgs/gamebg.png")no-repeat;*/
    background-color: black;
    overflow: auto;
}

</style>
