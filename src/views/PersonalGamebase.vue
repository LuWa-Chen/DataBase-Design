<template>
  <div>
    <head>
      <meta charset="UTF-8">
      <meta http-equiv="X-UA-Compatible" content="IE=edge">
      <meta name="viewport" content="width=device-width, initial-scale=1.0">
      <title>臻Game</title>
      <link rel="stylesheet" href="https://unpkg.com/swiper@7/swiper-bundle.min.css"/>
      <link rel="preconnect" href="https://fonts.googleapis.com">
      <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
      <link href="https://fonts.googleapis.com/css2?family=Jost:wght@100;200;300;400;500;600;700;800;900&family=Open+Sans:wght@300;400;500;600;700;800&display=swap" rel="stylesheet">
      <link rel="stylesheet" href="../assets/sea_css/index-style.css">
      <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
    </head>
    <game-page-header></game-page-header>
    <vue-particles
      color="#409EFF"
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
  <div class="back">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">

    <br><br><br>

    <h1 class="headplus">臻Gameg 用户游戏库</h1>
    <div align="right" ><a href="#">---进入游戏商店---&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;</a></div>
    <hr>
    <br><br><br>
    <div class="container">
      <div class="row">

        <router-link :to="{ path: '/GameDetail', query: {game_id:'0000000001'} }">
        <div class="col-md-3 col-sm-6">
          <div class="product-grid">
            <div class="product-image">
              <a href="#">
                <img class="pic-1" src="../assets/img/dead-cells.jpg">
                <img class="pic-2" src="../assets/img/dead-cells.jpg">
              </a>
              <span class="product-trend-label">已购买</span>
            </div>
            <div class="product-content">
              <h3 class="title"><a href="#">dead-cells</a></h3>
            </div>
          </div>
        </div>
        </router-link>

        <div class="col-md-3 col-sm-6">
          <div class="product-grid">
            <div class="product-image">
              <a href="#">
                <img class="pic-1" src="../assets/img/naraka.jpeg">
                <img class="pic-2" src="../assets/img/naraka.jpeg">
              </a>
              <span class="product-trend-label">已购买</span>
            </div>
            <div class="product-content">
              <h3 class="title"><a href="#">naraka</a></h3>
            </div>
          </div>
        </div>

        <div class="col-md-3 col-sm-6">
          <div class="product-grid">
            <div class="product-image">
              <a href="#">
                <img class="pic-1" src="../assets/img/back4bld.jpg">
                <img class="pic-2" src="../assets/img/back4bld.jpg">
              </a>
              <span class="product-trend-label">已购买</span>
            </div>
            <div class="product-content">
              <h3 class="title"><a href="#">back4bld</a></h3>
            </div>
          </div>
        </div>

        <div class="col-md-3 col-sm-6">
          <div class="product-grid">
            <div class="product-image">
              <a href="#">
                <img class="pic-1" src="../assets/img/far-cry-6-cover.png">
                <img class="pic-2" src="../assets/img/far-cry-6-cover.png">
              </a>
              <span class="product-trend-label">已购买</span>
            </div>
            <div class="product-content">
              <h3 class="title"><a href="#">far-cry-6</a></h3>
            </div>
          </div>
        </div>

        <div class="col-md-3 col-sm-6">
          <div class="product-grid">
            <div class="product-image">
              <a href="#">
                <img class="pic-1" src="../assets/img/final-fantasy.jpeg">
                <img class="pic-2" src="../assets/img/final-fantasy.jpeg">
              </a>
              <span class="product-trend-label">已购买</span>
            </div>
            <div class="product-content">
              <h3 class="title"><a href="#">final-fantasy</a></h3>
            </div>
          </div>
        </div>

        <div class="col-md-3 col-sm-6">
          <div class="product-grid">
            <div class="product-image">
              <a href="#">
                <img class="pic-1" src="../assets/img/tomb-raider.jpeg">
                <img class="pic-2" src="../assets/img/tomb-raider.jpeg">
              </a>
              <span class="product-trend-label">已购买</span>
            </div>
            <div class="product-content">
              <h3 class="title"><a href="#">tomb-raider</a></h3>
            </div>
          </div>
        </div>




      </div>
    </div>
    </div>
  </div>

</template>

<script>
import GamePageHeader from "@/views/GamePageHeader";

export default {

  name: 'stop-scroll',
  components: {
    GamePageHeader,

  }
}
</script>

<style scoped>
@import "../assets/sea_css/index-style.css";
@import "../assets/sea_css/mixins.css";
@import "../assets/sea_css/cards.css";
@import "../assets/sea_css/variables.css";
#particles-js {
  width: 100%;
  height: calc(100% - 100px);
  position: absolute;
}
:root{ --main-color: #e74c3c; }
.product-grid{
  font-family: 'Roboto', sans-serif;
  position: relative;
}
.product-grid .product-image{
  overflow: hidden;
  position: relative;
}
.product-grid .product-image:before{
  content: "";
  background: rgba(0,0,0,0.3);
  width: 100%;
  height: 100%;
  opacity: 0;
  position: absolute;
  top: 0;
  left: 0;
  z-index: 1;
  transition: all 0.4s ease-out 0s;
}
.product-grid:hover .product-image:before{ opacity: 1; }
.product-grid .product-image a{ display: block; }
.product-grid .product-image img{
  width: 100%;
  height: auto;
}
.product-image .pic-1{
  opacity: 1;
  backface-visibility: hidden;
  transition: all 0.4s ease-out 0s;
}
.product-grid:hover .product-image .pic-1{ opacity: 0; }
.product-image .pic-2{
  width: 100%;
  height: 100%;
  opacity: 0;
  backface-visibility: hidden;
  transform: scale(3);
  position: absolute;
  top: 0;
  left: 0;
  transition: all 0.4s ease-out 0s;
}
.product-grid:hover .product-image .pic-2 {
  opacity: 1;
  transform: scale(1);
}
.product-image .product-trend-label,
.product-image .product-discount-label{
  content: "";
  color: #fff;
  background-color: #2ecc71;
  font-size: 12px;
  line-height: 28px;
  font-weight: 700;
  text-align: center;
  text-transform: uppercase;
  padding: 0 7px;
  position: absolute;
  top: 12px;
  left: 12px;
  z-index: 3;
  border-radius: 50px;
}
.product-image .product-discount-label{
  background-color: var(--main-color);
  left: auto;
  right: 12px;
}
.product-grid .social {
  padding: 0;
  margin: 0;
  list-style: none;
  transform: translateY(-50%);
  position: absolute;
  top: 50%;
  left: 10px;
  z-index: 4;
}
.product-grid .social li {
  margin: 0 0 12px;
  opacity: 0;
  transform: translateX(-60px);
  transition: transform .3s ease-out 0s;
}
.product-grid:hover .social li {
  opacity: 1;
  transform: translateX(0);
}
.product-grid:hover .social li:nth-child(2){ transition-delay: 0.1s; }
.product-grid:hover .social li:nth-child(3){ transition-delay: 0.2s; }
.product-grid:hover .social li:nth-child(4){ transition-delay: 0.3s; }
.product-grid .social li a {
  color: #fff;
  font-size: 22px;
  transition: all 0.3s;
}
.product-grid .social li a:hover { color: var(--main-color); }
.product-grid .social li a:before,
.product-grid .social li a:after{
  content: attr(data-tip);
  color: #000;
  background: #fff;
  font-size: 14px;
  padding: 5px 10px;
  white-space: nowrap;
  display: none;
  transform: translateY(-50%);
  position: absolute;
  left: 33px;
  top: 50%;
  transition: all 0.3s;
}
.product-grid .social li a:after{
  content: '';
  background: linear-gradient(-45deg, #fff 49%, transparent 50%);
  width: 10px;
  height: 10px;
  top: 40%;
  left: 20px;
}
.product-grid .social li a:hover:before,
.product-grid .social li a:hover:after{
  display: block;
}
.product-grid .product-content{ padding: 12px 0; }
.product-grid .title{
  font-size: 15px;
  font-weight: 400;
  text-transform: capitalize;
  margin: 0 0 5px;
}
.product-grid .title a{ color: #959595; }
.product-grid .title a:hover{ color: var(--main-color); }
.product-grid .price{
  color: #333;
  font-size: 14px;
  font-weight: 400;
}
.product-grid .price span{
  color: #333;
  text-decoration: line-through;
  margin-right: 3px;
}
.product-grid .price.discount{ color: var(--main-color); }
@media only screen and (max-width:990px){
  .product-grid{ margin-bottom: 30px; }
}
.back{
  background-image:url(../assets/img/back.png) ;
}
.headplus{
  color:white;
  font-size: 30px;
  text-align:center;
}


</style>
