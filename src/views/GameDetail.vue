<!--张宇 1952168-->
<template>
    <div class="canvas">
        <game-page-header></game-page-header>
        <vue-particles
          color="#00FFFF"
          :particleOpacity="0.7"
          :particlesNumber="50"
          shapeType="star"
          :particleSize="6"
          linesColor="#409EFF"
          :linesWidth="1"
          :lineLinked="true"
          :lineOpacity="0.4"
          :linesDistance="100"
          :moveSpeed="1"
          :hoverEffect="true"
          hoverMode="grab"
          :clickEffect="true"
          clickMode="repulse">
        </vue-particles>
<div style="position:relative">
	
	     <div style="height: 50px"></div>
        <GameCarousel :game_id="this.$route.query.game_id"></GameCarousel>
        <div style="height: 50px"></div>
        <GameInfo :game_id="this.$route.query.game_id"></GameInfo>
        <div style="height: 50px"></div>
        <GameRecommended :game_id="this.$route.query.game_id"></GameRecommended>
        <div style="height: 50px"></div>
        <GameComments :game_id="this.$route.query.game_id"></GameComments>
        <div style="height: 50px;clear: both"></div>
<GamePageFooter/>
</div>
         
<!--        <div style="height: 50px"></div>-->
<!--        <GameCarousel game_id="0000000006"></GameCarousel>-->
<!--        <div style="height: 50px"></div>-->
<!--        <GameInfo game_id="0000000006"></GameInfo>-->
<!--        <div style="height: 50px"></div>-->
<!--        <GameRecommended game_id="0000000006"></GameRecommended>-->
<!--        <div style="height: 50px"></div>-->
<!--        <GameComments game_id="0000000006"></GameComments>-->
<!--        <div style="height: 50px"></div>-->
        </div>
    </div>
</template>

<script>
import GamePageHeader from "@/views/GamePageHeader";
import GamePageFooter from "@/views/GamePageFooter";
import GameCarousel from "@/components/GameCarousel";
import GameInfo from "@/components/GameInfo";
import GameRecommended from "@/components/GameRecommended";
import GameComments from "@/components/GameComments";
export default {
    name: "GameDetail",
    props:['game_id'],
    components: {
        GamePageHeader,
        GamePageFooter,
        GameCarousel,
        GameInfo,
        GameRecommended,
        GameComments,
    },
    watch: {
      '$route' () {
        location.reload();
      }
    },
}

</script>

<style scoped>
#particles-js {
  width: 100%;
  height: calc(270% - 100px);
  position: absolute;
}
.canvas {
    position:fixed;
    width:100%;
    height:100vh;
    /*background: url("../assets/imgs/gamebg.png")no-repeat;*/
    background-color: black;
    overflow: auto;
    color:black,
}

</style>
