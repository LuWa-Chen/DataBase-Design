<template>
  <div class="shopping">
    <div class="shopping-header">
      <div class="c-wrapper">
        <img :src="require('../../../../ExGame-Asset/Game/'+game_id+'/Cover/icon.png')" width="60" height="60">
        <div class="text-wrapper">
          <h1>{{ gamename }}</h1>
          <span v-for="(tag, index) in tags" :key="index">{{ tag }}</span>
        </div>
      </div>
    </div>
    <div class="shopping-main">
      <el-button>开始游戏</el-button>
    </div>
  </div>
</template>

<script>

export default {
  name: 'shopping',
  props: ['gamename', 'tags', 'game_id']
}
</script>

<style>
.shopping {
  border-top: 1px solid #585757;
  width: 500px;
  height: 250px;
  background: linear-gradient(to bottom,#272e3b,#1c2534);
  position: relative;
  left: 150px;
  top: 100px;
}
.shopping-header {
  border-bottom: 1px solid rgba(0,0,0,.3);
  width: 500px;
  height: 101px;
}
.c-wrapper {
  position: absolute;
  left: 30px;
  top: 18px;
}
.text-wrapper {
  display: inline-block;
  color: white;
  margin-left: 10px;
}
.text-wrapper h1 {
  font-family: inherit;
  font-size: 24px;
  height: 40px;
  font-weight: 500;
  position: relative;
  left: 14px;
}
.text-wrapper span {
  color: rgba(255, 255, 255, 0.8);
  margin-left: 15px;
  margin-top: 5px;
  font-family: "Open Sans",Helvetica,Arial,sans-serif;
  font-size: 14px;
}
.shopping-main .el-button{
  width: 456px;
  height: 61px;
  display: block;
  border-radius: 0;
  margin: 35px auto;
  background-color: #0e86ca;
  border-color: #10a1de;
  font-size: 1.25rem;
  color: white;
}
.shopping-main .el-button:hover {
  background-color: #119ef3;
  color: white;
}
</style>
