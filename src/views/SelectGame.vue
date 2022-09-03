<template>
  <div class="profilePage">
        <el-card>
          <div class="top-text">已发行的游戏</div>
          <el-table
              :data="games"
              border
              style="width: 88%"
              :header-cell-style="{
          'text-align': 'center',
          background: '#F5F5F5'
        }"
              :cell-style="{
          'text-align': 'center'
        }"
              max-height="500"
              empty-text="您还没有发行的游戏，快去发行吧~~~">
            <el-table-column
                fixed
                prop="image"
                label="封面"
                width="110">
              <template slot-scope="scope">
                <img :src="scope.row.image" width="80" height="80" alt="封面"/>
              </template>
            </el-table-column>
            <el-table-column
              prop="game_name"
              label="游戏名"
              width="150">
          </el-table-column>
            <el-table-column
                prop="pre_price"
                label="原价"
                width="113">
            </el-table-column>
            <el-table-column
                prop="tag"
                label="游戏类型"
                width="300">
            </el-table-column>
            <el-table-column
                prop="publishDate"
                label="发行日期"
                width="150">
            </el-table-column>
            <el-table-column
                label="操作"
                width="300">
              <!-- 查看按钮 -->
              <template slot-scope="scope">
                <!-- 发布推送按钮 -->
                  <el-button
                      type="primary"
                      size="mini"
                      icon="el-icon-s-promotion"
                      @click="DialogVisible_1 = true; gameID = games[scope.$index].game_id"
                  >发布推送</el-button>
                <br>
                <!-- 发布折扣按钮 -->
                <el-button
                    type="success"
                    size="mini"
                    icon="el-icon-s-finance"
                    @click="DialogVisible_2 = true; gameName = scope.row.game_name"
                >发布折扣</el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-card>
    <PublishNews :game_id="gameID" :DialogVisible="DialogVisible_1" @callBack="callBack_1"></PublishNews>
    <PublishDiscount :game_name="gameName" :DialogVisible="DialogVisible_2" @callBack="callBack_2"></PublishDiscount>
      </div>
</template>

<script>
import { showLoading, hideLoading } from '@/components/LoadingSet/loading.js';
import PublishNews from './PublishNews'
import PublishDiscount from "./PublishDiscount";
export function formatNum(num){
  return num < 10 ? "0" + num : num
}
export default {
  name: "SelectGame",
  data() {
    return {
      gameName:'',
      gameID:'',
      DialogVisible_1: false,
      DialogVisible_2: false,
      games:[],
      iframeData: {
        id: this.$route.query.id,
        name:this.$store.state.userName,
        game_count:this.$route.query.game_count,
        result:this.$route.query.result,
      }
    }
  },
  methods: {
    // 实时-时间-数据转换
    formatDate(value) {
      let year = value.getFullYear()
      let month = formatNum(value.getMonth() + 1)
      let day = formatNum(value.getDate())
      return year + "/" + month + "/" + day
    },
    callBack_1(flag){
      this.DialogVisible_1 = flag
    },
    callBack_2(flag){
      this.DialogVisible_2 = flag
    }
  },
  components:{
    PublishNews,
    PublishDiscount
  },
  created() {
    const self = this;
    showLoading()
    self.$axios({
      method: 'post',
      url: 'api/publisher/getgameinfo',
      data: {
        publisher:this.$store.state.userName,
      }
    })
        .then(res => {
          hideLoading()
          switch (res.data.result) {
            case 1:
              for(let i=0;i<res.data.game_count;i++)
              {
                this.$data.games.push({
                  game_id:res.data.games[i].game_id,
                  game_name:res.data.games[i].game_name,
                  pre_price:res.data.games[i].pre_price.toFixed(2),
                  now_price:res.data.games[i].now_price.toFixed(2),
                  tag:res.data.games[i].tag,
                  publishDate:this.formatDate(new Date(res.data.games[i].publish_date)),
                  image:require('../../../ExGame-Asset/Game/'+ res.data.games[i].game_id +'/Cover/Cover.jpg')})
              }
              console.log("查询成功！");
              break;
            case 0:
              console.log("查询失败！");
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
  overflow: scroll;

}
.top-text{
  margin:10px 50px;
  font-size: 20px;
}
.el-button{
  margin-top: 6px;
}

.el-table{
  margin: 0 0 30px 50px;
  font-size: small;
}
.el-card{
  margin: 50px;
}
.el-row{
  margin: 30px
}
</style>
