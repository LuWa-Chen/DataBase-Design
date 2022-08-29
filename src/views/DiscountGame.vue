<template>
  <div class="profilePage">
    <br><br>
    <div class="top-text"><font color="white" class="toptext">{{iframeData.name}}的折扣游戏</font></div>
    <div class="explain-text">
      <ul>
        <li v-for="(game,index) in iframeData.games" v-if="game.discount<1">
          <div>
            <font color="white" class="exltext">游戏名：{{game.game_name}}</font>
          </div>
          <br>
          <div>

            <font color="white" class="exltext">原价：{{game.pre_price}}$</font>
          </div>
          <br>
          <div>
            <font color="red" class="exltext">现价：{{game.now_price}}$</font>

          </div>
          <br>
          <div>
            <font color="white" class="exltext">游戏类型：{{game.tag}}</font>
          </div>
          <br>
          <font color="white" class="exltext">游戏发行日期：{{game.publish_date}}</font>
          <br>


          <div>
            <img class="picture_item" :src="game.image" alt="发行商发行游戏图片" />
          </div>
          <br>
          <br>
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
export function getZero(num){
  if(parseInt(num) < 10){
    num = '0' + num;
  }
  return num;
}
export default {
  name: "Discountgame",
  data() {
    return {
      iframeData: {
        id: this.$route.query.id,
        name:this.$store.state.userName,
        games:this.$route.query.games,
        game_count:this.$route.query.game_count,
        result:this.$route.query.result,
      }
    }
  },
  methods: {

  },
  created() {
    const self = this;
    self.$axios({
      method: 'post',
      url: 'api/publisher/getgameinfo',
      data: {
	publisher:this.$store.state.userName,
      }
    })
        .then(res => {
          switch (res.data.result) {
            case 1:
              alert("查询成功！");
              break;
            case 0:
              alert("查询失败！");
              break;
            case -1:
              alert("数据库连接失败！");
              break;
          }
        })
        .catch(err => {
          console.log(err);
        })
  }
}
</script>

<style scoped>
.profilePage{
  margin: 0;
  padding: 0;
  background:url("../assets/background.jpg");
  width:100%;
  height:100%;
  position:relative;
  background-size:100% 100%;
}
.top-text{
  margin:0px 120px 0;
  font-size: 25px;

  font-family: 幼圆;
}
.explain-text{
  margin:40px 120px 0;
  font-size: 20px;
}

button{
  width: 60px;
  height: 40px;
  line-height: 1rem;
  white-space: normal;
  border: thin solid #c8c8c8;
  box-sizing: border-box;
  border-radius: 2px;
  background-color: #fff;
  position: absolute;
  right: 0;
}
button:hover{
  background-color: #F5F5F5;
}
.picture_item{
  line-height: 1.8rem;
  width: 25%;
  position: relative;
}
.exltext
{
  font-family: 幼圆;
}
.toptext
{
  font-family: 幼圆;
  margin-top:10px;
}
</style>