<template>
  <div class="about">
    <vhead/>
    <headtow/>
    <div class="about_main">
      <img class="icon" :src="require('../../../../ExGame-Asset/Game/'+game_id+'/Cover/cover.jpg')">
      <div class="shop">
        <shopping :gamename="gmname" :tags="tags" :game_id="game_id"></shopping>
      </div>
    </div>
    <pro :intro="intro" :news="news" :game_id="game_id"></pro>
  </div>
</template>
<script>
import vhead from '../components/Header/Header.vue'
import headtow from '../components/Header/headTow.vue'
import pro from '../components/GameDetail.vue'
import shopping from '../components/shopping.vue'
export default{
  name: 'About',
  components: {
    vhead,
    headtow,
    shopping,
    pro
  },
  data () {
    return {
      cover: '',
      bgimg: '',
      gmname: '',
      game_id: '',
      intro: '',
      tags: [],
      news: [
        {
          post_cover: '',
          post_title: '',
          content_text: '',
          post_time: '',
        },
        {
          post_cover: '',
          post_title: '',
          content_text: '',
          post_time: ''
        },
        {
          post_cover: '',
          post_title: '',
          content_text: '',
          post_time: '',
        },
        {
          post_cover: '',
          post_title: '',
          content_text: '',
          post_time: ''
        },
      ]
    }
  },
  created() {
    this.game_id = this.$route.query.game_id;
  },
  watch: {
    game_id: function(val) {
      this.getGmname(val);
      this.getGameTags(val);
      this.getGameCover(val);
      this.getGameNews(val, 1);
      this.getGameNews(val, 2);
      this.getGameNews(val, 3);
      this.getGameNews(val, 4);
      this.getGameIntro(val);
    },
    immediate: true
  },
  methods: {
    getGmname (val) {
      this.$axios
        .post('api/library/getGameName', {
          game_id: val
        })
        .then(res => {
          this.gmname = res.data.game_name
        })
        .catch(error => {
          console.log(error)
        })
    },
    getGameCover(val){
      this.$axios
          .post('api/getGameInfo', {
            game_id: val
          })
          .then(res => {
            this.tags = res.data.tag
          })
          .catch(error => {
            console.log(error)
          })
    },
    getGameTags(val){
      this.$axios
          .post('api/getGameInfo', {
            game_id: val
          })
          .then(res => {
            this.cover = res.data.cover
          })
          .catch(error => {
            console.log(error)
          })
    },
    getGameIntro(val){
      this.$axios
          .post('api/getGameInfo', {
            game_id: val
          })
          .then(res => {
            this.intro = res.data.general_intro
          })
          .catch(error => {
            console.log(error)
          })
    },
    getGameNews(val1, val2){
      this.$axios
          .post('api/library/getGameNews', {
            game_id: val1,
            post_index: val2
          })
          .then(res => {
            this.post_cover = res.data.post_cover;
            this.news[val2-1].post_cover = res.data.post_cover;
            this.news[val2-1].post_title = res.data.post_title;
            this.news[val2-1].content_text = res.data.content_text;
            this.news[val2-1].post_time = res.data.post_time;
          })
          .catch(error => {
            console.log(error)
          })
    }
  }
}
</script>
<style scoped>
.about{
  background-color: #002650;
}
.about_main{
  background-image:url('#') ;
  transform: translate3d(0px, 0px, 0px);
  position: relative;
  box-shadow: inset 0 -30px 60px rgba(0,0,0,.3);
  width: 100%;
  height: 600px;
  display: block;
}
.about_main .shop{
  position: absolute;
  right: 190px;
  top: 290px;
}
.about_main .icon{
  position: absolute;
  height: 600px;
  width: 100%;
  /*top: 100px;*/
  /*right: 265px;*/
}
.el-tabs__nav{
  margin-left: 15px;
}
</style>

