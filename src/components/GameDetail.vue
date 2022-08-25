<template>
  <div>
    <div class="product_media">
      <div class="left_media">
        <div class="product_title " ><h2>难以置信的简单、不可思议的有趣</h2></div><br>
        <p>{{ intro }}</p>
      </div>
    </div>

    <div class="product-feature clearall">
      <div class="product_title"><h2>最新新闻</h2></div>
      <div class="product_box">
        <div  class="product_imgs" v-for="(item,index) in news" :key='index'>
          <router-link :to="{ path: '/News', query: {game_id: game_id, post_index: index} }">
          <img  class="product_img" :src="require('../../../../ExGame-Asset/Game/'+game_id+'/News/new'+parseInt(index+1)+'.jpg')">
          <h5>{{ item.post_title | ellipsisTitle }}</h5>
          <p>{{ item.content_text | ellipsis }}</p>
          </router-link>
        </div>
      </div>
    </div>

    <div class="product_info clearall">
      <div class="product_data">
        <div class="product_title"><h2>商品详情</h2></div>
        <div class="product_container">
          <div class="grid-content" v-for="(item, index) in htitle" :key="index">
            <h3>{{ item.title }}</h3>
            <p v-for="(info, key) in item.infos" :key="key">
              <i style="float: left;margin-right: 10px;" v-if="item.title != '类型'">•</i>
              {{ info }}
            </p>
          </div>
        </div>
      </div>
    </div>

    <div class="comment">
      <div class="product_title"><h2>评测</h2></div>
      <div class="rate">
        <p>请为该游戏打分（范围为1-5，1分是特别不推荐，5分是特别推荐）</p>
        <star-rating @rating-selected = "setRating"></star-rating>
      </div>
      <div class="description">
        <p>请描述您为什么喜欢或不喜欢该产品，以及您是否愿意向他人推荐此游戏。请注意礼貌用词，并遵循规则及指引。</p>
        <textarea v-model="item" cols="115" rows="5"></textarea>
      </div>
      <el-button @click="commit" type="primary" round>发布评测</el-button>
    </div>
  </div>
</template>

<script>
import StarRating from "vue-star-rating";
export default {
  name: 'GameDetail',
  components: {StarRating},
  props: ['news', 'intro', 'game_id'],
  filters: {
    ellipsis(value) {
      if (!value) return "";
      if (value.length > 30) {
        return value.slice(0,30) + "...";
      }
      return value;
    },
    ellipsisTitle(value) {
      if (!value) return "";
      if (value.length > 15) {
        return value.slice(0,15) + "...";
      }
      return value;
    }
  },
  data () {
    return {
      item: '',
      rating: 0,
      activename: 'Windows',
      htitle: [
        {
          title: '类型',
          infos: ['游戏']
        },
        {
          title: '支持语言',
          infos: ['简体中文', '繁体中文']
        },
        {
          title: '商品要求',
          infos: ['需要互联网连接', '需要游戏通行证']
        },
        {
          title: '平台',
          infos: ['Windows', 'iOS', 'Android']
        },
        {
          title: '可使用的地区',
          infos: ['中国']
        }
      ],
      systems: ['Windows', 'iOS', 'Android']
    }
  },
  methods: {
    setRating: function(rating){
      this.rating = rating;
    },
    commit(){
      // eslint-disable-next-line no-empty
        if (this.item != ''){
          this.$axios
              .post('api/library/comment', {
                user_id: '0000000014',
                game_id: this.game_id,
                rating: this.rating,
                content: this.item
              })
              .then(res => {
                switch(res.data.result){
                  case 0: alert("发布成功");
                        break;
                  case -1: alert("请勿重复发布")
                        break;
                }
              })
              .catch(error => {
                console.log(error)
              })
        } else {
          alert("填写不能为空！");
        }
    }
  }
}
</script>

<style scoped>
.clearall::after{
  content: "";
  display: block;
  clear: both;
}
.product_media,
.product-feature,
.product_info,
.comment{
  position: relative;
  display: block;
  height: 230px;
  padding-top: 60px;
  border-bottom: 1px solid rgba(255, 255, 255, 0.15);
  color: rgba(255, 255, 255, 0.8);
  min-width: 1600px;
  padding-bottom: 50px;
}
.product-feature{
  height: 800px;
}
.comment{
  height: 400px;
}
.product-feature .product_imgs{
  width: 420px;
  float: left;
  min-height: 1px;
  box-shadow: 5px 5px 10px rgba(0,0,0,.3);
  margin-bottom: 100px;
  margin-left: 35px;
  margin-right: 35px;
  position: relative;
  left: -410px;
  top: 20px;
}
.product_media .left_media{
  position: relative;
  top: 100px;
  width: 100%;
  line-height: 25px;
}
.product_media .left_media p{
  margin-left: 120px;
  margin-right: 500px;
}
.right_media ul li{
  margin-bottom: 10px;
}
.right_media ul li a{
  text-decoration: none;
  transition: color .3s;
  color: rgb(0, 174, 255);
}
.product-feature .product_box{
  font-size: 16px;
  margin-left: 495px;
  font-weight: 400;
  font-family: "Open Sans", Helvetica, Arial, sans-serif;
  height: 1000px;
}
a {
  text-decoration: none;
}
.product_box .product_imgs p{
  color: white;
  margin-top: 10px;
  margin-bottom: 20px;
  display: block;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}
.product_box .product_imgs h5{
  color: white;
  margin-top: 10px;
  margin-bottom: 10px;
  font-size: 20px;
  font-weight: 400;
}
.product_imgs .product_img{
  width: 420px;
  height: 200px;
}
.product_title h2 {
  color: #f8f9fa;
  font-size: 28px;
  font-weight: 300;
  margin-left: -70px;
}

.product_title {
  padding-left: 190px;
}

.product_container {
  float: right;
  min-width: 100%;
  width: 100px;
  padding-left: 20px;
  position: relative;
  left: -110px;
  top: 30px;
}

.product_container h3 {
  color: rgba(255, 255, 255, 0.8);
  font-weight: 500;
  line-height: 1.2;
  margin-bottom: 10px;
}
.product_container .grid-content {
  float: left;
  min-width: 300px;
  width: 300px;
  height: 90px;
  left: -70px;
  position: relative;
}
.product_container .grid-content p {
  font-size: 14px;
  font-weight: 400;
  line-height: 25px;
}
.right_ft a {
  text-decoration: none;
  color: #00aeff;
}
.right_ft p {
  margin-top: 10px;
  font-size: 14px;
  color: #ffffff;
}
.el-tab-pane span {
  color: #ffffff;
  font-size: 16px;
}
.tabs .left_win {
  float: left;
  width: 50%;
}
.comment .rate {
  margin-top: 20px;
  margin-left: 120px;
  margin-right: 500px;
}
.comment .rate p{
  margin-bottom: 15px;
}
.comment .description {
  margin-top: 50px;
  margin-left: 120px;
  margin-right: 500px;
}
.comment .description p {
  margin-bottom: 15px;
}
.comment .description textarea {
  font-size: 16px;
  line-height: 20px;
}
.comment .el-button {
  margin-top: 20px;
  margin-left: 120px;
  margin-right: 500px;
}
</style>
