<template>
  <div id="news">
    <vhead></vhead>
    <headtow></headtow>
    <div class="title">
      <h1>{{post_title}}</h1>
    </div>
    <div class="time">
      <p>{{post_time}}</p>
    </div>
    <div class="img">
      <img :src="require('../../../../ExGame-Asset/Game/'+game_id+'/News/new'+post_index+'.jpg')" width="900px" height="400px">
    </div>
    <div class="paragraph">
      <p>&nbsp;&nbsp;&nbsp;&nbsp;{{content_text}}</p>
    </div>
  </div>
</template>

<script>
import vhead from "@/components/Header/Header";
import headtow from "@/components/Header/headTow";

export default {
  name: "News",
  components: {
    vhead,
    headtow,
  },
  data() {
    return {
      game_id: '',
      post_index: -1,
      post_cover: '',
      post_title: '',
      content_text: '',
      post_time: ''
    }
  },
  created() {
    this.game_id = this.$route.query.game_id;
    this.post_index = parseInt(this.$route.query.post_index) + 1;
  },
  computed: {
    changeData() {
      const { game_id, post_index} =this
      return {
        game_id,
        post_index
      }
    }
  },
  watch: {
    changeData: function (val) {
      console.log(val.post_index)
      this.getGameNews(val.game_id, val.post_index)
    }
  },
  methods: {
    getGameNews(val1, val2){
      this.$axios
          .post('api/library/getGameNews', {
            game_id: val1,
            post_index: val2
          })
          .then(res => {
            this.post_cover = res.data.post_cover;
            this.post_title = res.data.post_title;
            this.content_text = res.data.content_text;
            this.post_time = res.data.post_time;
          })
          .catch(error => {
            console.log(error)
          })
    }
  }
}
</script>

<style scoped>
#news{
  background-color: #002650;
  height: auto;
}
.title h1 {
  font-size: 30px;
}
.time,
.img,
.title,
.paragraph{
  position: relative;
  display: block;
  color: rgba(255, 255, 255, 0.8);
  margin-top: 20px;
  margin-left: 30px;
  margin-bottom: 20px;
  width: 900px;
}
.paragraph{
  min-height: 200px;
  top: 20px;
  bottom: 20px;
}
.paragraph p{
  line-height: 20px;
}
</style>
