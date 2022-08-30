<!--1952168 张宇-->
<template>
    <div class="bb">
        <div style="width: 1005px;margin: auto;">
            <div class="m-tt fl">用户测评</div>
            <div class="line4 clearbox"></div>
            <div class="m-page clearbox" >
                <div class="comment-filter  clearbox">
                    <div class="fl">
                        <div class="filter-box" style="height: 80px" @click="click2Choose(1)">
                            <div class="filter-title clearbox">
                                测评结果
                                <select  disabled class="filter-bar fr"></select>
                            </div>
                            <div>
                                <div  v-if="filters[0].cur===0" style="margin-left: 10px;margin-top:5px;font-size: 20px;font-weight: bolder">
                                    全部
                                </div>
                                <div class="comment-rate" v-else>
                                    <div class="m-rate-left fl" style="margin-top: 8px">
                                        <div v-if="filters[0].cur===1" style="text-indent: -25px;height: 25px;width: 25px;overflow: hidden">
                                            <img  src="../assets/imgs/emoji-laughing.svg" style="filter: drop-shadow(25px 0px orange)" height="25" alt="">
                                        </div>
                                        <div v-else >
                                            <img src="../assets/imgs/emoji-frown.svg" height="25" alt="">
                                        </div>
                                    </div>
                                    <div class="m-rate-right fl" style="margin-top: 10px">
                                        <div class="rate-name" >
                                            <span v-if="filters[0].cur===1" style="color: orange">推荐</span>
                                            <span v-else>不推荐</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <ul id="s1"  class="select-bar" style="z-index: 999;position: absolute;display: none">
                            <li id="s1l1" @click="filterChoose(1,1)">&nbsp;&nbsp;{{ filters[0].valueList[0] }}</li>
                            <li id="s1l2" @click="filterChoose(1,2)">&nbsp;&nbsp;{{ filters[0].valueList[1] }}</li>
                            <li id="s1l3" @click="filterChoose(1,3)">&nbsp;&nbsp;{{ filters[0].valueList[2] }}</li>
                        </ul>
                    </div>
                    <div class="fl">
                        <div>
                            <div class="filter-box"  @click="click2Choose(2)">
                                <div class="filter-title">
                                    获得方式
                                    <select  disabled  class="filter-bar fr"></select>
                                    <span class="fr">{{ filters[1].valueList[filters[1].cur] }}</span>
                                </div>
                            </div>
                            <ul id="s2"   class="select-bar" style="z-index: 999;position: absolute;display: none">
                                <li id="s2l1" @click="filterChoose(2,1)">&nbsp;&nbsp;{{ filters[1].valueList[0] }}</li>
                                <li id="s2l2" @click="filterChoose(2,2)">&nbsp;&nbsp;{{ filters[1].valueList[1] }}</li>
                                <li id="s2l3" @click="filterChoose(2,3)">&nbsp;&nbsp;{{ filters[1].valueList[2] }}</li>
                            </ul>
                        </div>
                        <div>
                            <div class="filter-box"  @click="click2Choose(3)">
                                <div class="filter-title">
                                    时间
                                    <select  disabled  class="filter-bar fr"></select>
                                    <span class="fr">{{ filters[2].valueList[filters[2].cur] }}</span>
                                </div>
                            </div>
                            <ul id="s3"  class="select-bar"  style="z-index: 999;position: absolute;display: none">
                                <li id="s3l1" @click="filterChoose(3,1)">&nbsp;&nbsp;{{ filters[2].valueList[0] }}</li>
                                <li id="s3l2" @click="filterChoose(3,2)">&nbsp;&nbsp;{{ filters[2].valueList[1] }}</li>
                                <li id="s3l3" @click="filterChoose(3,3)">&nbsp;&nbsp;{{ filters[2].valueList[2] }}</li>
                            </ul>
                        </div>

                    </div>
                </div>
                <div class="tab-bar clearbox">
                    <span class="comment-count fl">找到{{commentNum}}条测评</span>
                    <ul class="comment-tab fr">
                        <li @click="tab(index)"  v-for="(item,index) in items"
                            :class="{active : index===curId}" :key="index" style="display: inline;margin-left: 20px">{{item.item}}
                        </li>
                    </ul>
                </div>
                <div class="tab-con clearbox">
                    <div style="height: 20px"></div>
                    <div class="comment-tip1"  v-show="curId===0&&comments.length!==0">
                        <span class="line1"></span>
                        &emsp;10天内的测评&emsp;
                        <span class="line2"></span>
                    </div>
                    <div  class="comment-tip2" v-show="comments.length===0">暂无评论</div>
                    <ul class="most-value fl">
                        <li v-for="(comment,index) in comments" :key="index">
                            <div class="m-comment fl">
                                <div class="comment-creator fl">
                                    <div class="user-head">
                                        <img  :src="require('../assets/imgs/' + comment.user.head)"  alt="" height="60" width="60" >
                                    </div>
                                    <div class="user-info">
                                        <p>{{comment.user.name}}</p>
                                        <p>账户内拥有{{comment.user.gameNum}}项产品</p>
                                        <p>{{comment.user.commentNum}}篇测评</p>
                                    </div>
                                </div>
                                <!--                    </div>-->
                                <div class="m-comment-right fr">
                                    <div class="comment-rate">
                                        <div class="m-rate-left fl">
                                            <div v-if="comment.detail.rate>=3" style="text-indent: -25px;height: 25px;width: 25px;overflow: hidden">
                                                <img  src="../assets/imgs/emoji-laughing.svg" style="filter: drop-shadow(25px 0px orange)" height="25" alt="">
                                            </div>
                                            <div v-else >
                                                <img src="../assets/imgs/emoji-frown.svg" height="25" alt="">
                                            </div>
                                        </div>
                                        <div class="m-rate-right fl">
                                            <div class="rate-name" >
                                                <span v-if="comment.detail.rate>3">推荐</span>
                                                <span v-else>不推荐</span>
                                            </div>
                                        </div>
                                        <div class="rate-count-time fr">
                                            于 {{comment.user.accessTime}}日获得该游戏
                                            （通过
                                            <span v-if="comment.user.viaCDK===1">CKD</span>
                                            <span v-else>购买</span>
                                            获得）
                                        </div>
                                        <div class="line3 clearbox"></div>
                                    </div>
                                    <div class="comment-main" >
                                        <div class="comment-time">
                                            发布于：{{comment.detail.date}}
                                        </div>
                                        <div class="wrapper">
                                            <input :id="getID(index+1,'exp')" class="exp"  type="checkbox">
                                            <div class="text">
                                                <label class="btn" :for="getID(index+1,'exp')"></label>
                                                {{comment.detail.content}}
                                            </div>
                                        </div>
                                    </div>
                                    <div class="comment-judge" >
                                        <div class="rate-icons" v-show="curId===0">
                                            <span class="rate-ico"  :id="getRateID(index+1,'rate-ico-1-',1)" @click="addRate(index,1,1)">有用（{{comment.rate.goodCount}}）</span>
                                            <span class="rate-ico" :id="getRateID(index+1,'rate-ico-2-',1)" @click="addRate(index,2,1)">没用（{{comment.rate.badCount}}）</span>
                                        </div>
                                        <div class="rate-icons" v-show="curId===1">
                                            <span class="rate-ico"  :id="getRateID(index+1,'rate-ico-1-',2)" @click="addRate(index,1,2)">有用（{{comment.rate.goodCount}}）</span>
                                            <span class="rate-ico" :id="getRateID(index+1,'rate-ico-2-',2)" @click="addRate(index,2,2)">没用（{{comment.rate.badCount}}）</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
                <div class="more-comments clearbox" @click="getMoreComments()" v-show="commentNum>0&&this.isEnd===false">
                    <span > 查看更多测评 <i class="icon-downarrow"></i></span>
                </div>
                <div class="end-comments clearbox" v-if="commentNum>0&&this.isEnd===true" >
                    <span>已经到尽头了...</span>
                </div>
            </div>
        </div>
        <div style="height: 20px"></div>
    </div>
</template>

<script>
export default {
    name: "GameComments",
    props:['game_id'],
    data(){
        return {
            filters:[
                {
                    cur:0,
                    value:'测评结果',
                    valueList:['全部','推荐','不推荐']
                },
                {
                    cur:0,
                    value:'获得方式',
                    valueList:['全部','购买','其它']
                },
                {
                    cur:0,
                    value:'时间',
                    valueList:['全部','最近一周','最近一个月']
                },
            ],
            curId: 0,
            pgn:1,
            isEnd:false,
            items: [
                {item: '最有价值'},
                {item: '最新发表'},
            ],
            commentNum:0,
            mostValuedComments:[],
            mostRecentComments:[],
            comments:[],
        }
    },
    mounted() {
        this.getData(this.game_id,'0000000001',1,1,0,0,0);
        setTimeout(this.init,600);
        setTimeout(this.pic,610);
    },
    methods:{
        click2Choose(index){
            var id = 's' + index.toString()
            var ss  = document.getElementById(id)
            if(ss.style.display === 'block')
            {
                ss.style.display = 'none'
            }
            else
            {
                ss.style.display = 'block';
            }
        },
        filterChoose(i,ii){
            for(let j = 0;j<this.filters[i-1].valueList.length;j++)
            {
                const temp = 's' + i.toString() + 'l' + (j + 1).toString();
                let ft = document.getElementById(temp)
                ft.style.backgroundColor = 'white'
            }
            const id = 's' + i.toString() + 'l' + ii.toString();
            let fl = document.getElementById(id)
            fl.style.backgroundColor = '#e9e9e9'
            this.filters[i-1].cur  = ii-1;
            this.click2Choose(i)
            this.pgn =1;
            this.getData(this.game_id,'0000000001',1,this.curId+1,this.filters[0].cur,this.filters[1].cur,this.filters[2].cur);
            setTimeout(this.init,600);
            setTimeout(this.pic,610);
            // this.comments = this.funFilter(this.comments,i,ii);
        },
        tab (index) {
            if(index===0)
            {
                this.comments = this.mostValuedComments;
            }
            else {
                this.comments = this.mostRecentComments;
            }
            console.log(this.curId.toString() + '+++++')
            this.curId = index;
            this.pgn =1;
            this.commentNum = 0
            this.getData(this.game_id,'0000000001',1,this.curId+1,this.filters[0].cur,this.filters[1].cur,this.filters[2].cur);
            setTimeout(this.init,600);
            setTimeout(this.pic,610);
            // this.pic();
        },
        getMoreComments:function (){
            // this.getData('0000000006','0000000008');
            if(this.isEnd===false)
            {
                this.pgn++;
                this.getData(this.game_id,'0000000001',this.pgn,this.curId+1,this.filters[0].cur,this.filters[1].cur,this.filters[2].cur);
                setTimeout(this.apd,1000);
                setTimeout(this.pic,1010);
            }
        },
        getData: function (gid,uid,pgn,type,f1,f2,f3)
        {
            if(gid.length===0)
            {
                alert('gid 不能为空')
                return;
            }
            if(uid.length===0)
            {
                alert('uid 不能为空')
                return;
            }
            console.log('getData')
            var self = this;
            self.mostValuedComments = [];
            self.mostRecentComments = [];
            this.$axios.post('api/gamedetail/getGameComments', {
                game_id: gid,
                user_id:uid,
                page_no:pgn,
                comment_type:type,
                filter_1:f1,
                filter_2:f2,
                filter_3:f3,
            }).then( res => {
                console.log('本次获取 ' + res.data.result.toString() +" 条评论")
                self.commentNum = res.data.comment_num;
                self.isEnd = res.data.is_end;
                if(type===1)
                {
                    for(let i in res.data.comment_list)
                    {
                        console.log('comment -----' + res.data.comment_list[i].id)
                        self.mostValuedComments.push(
                            {
                                user:{
                                    head:"h1.jpg",
                                    uid:res.data.comment_list[i].id,
                                    name:res.data.comment_list[i].name,
                                    gameNum:res.data.comment_list[i].game_num,
                                    commentNum:res.data.comment_list[i].comment_num,
                                    accessTime:res.data.comment_list[i].access_time,
                                    viaCDK:res.data.comment_list[i].via_cdk,
                                },
                                detail:{
                                    rate:res.data.comment_list[i].rate,
                                    date:res.data.comment_list[i].date,
                                    content:res.data.comment_list[i].content
                                },
                                rate:{
                                    goodCount:res.data.comment_list[i].good_count,
                                    badCount:res.data.comment_list[i].bad_count,
                                },
                                myChoice:{
                                    good:res.data.comment_list[i].good,
                                    bad:res.data.comment_list[i].bad,
                                }
                            }
                        )
                    }
                }
                else
                {
                    for(let i in res.data.comment_list)
                    {
                        console.log('comment -----' + res.data.comment_list[i].id)
                        self.mostRecentComments.push(
                            {
                                user:{
                                    head:"h1.jpg",
                                    uid:res.data.comment_list[i].id,
                                    name:res.data.comment_list[i].name,
                                    gameNum:res.data.comment_list[i].game_num,
                                    commentNum:res.data.comment_list[i].comment_num,
                                    accessTime:res.data.comment_list[i].access_time,
                                    viaCDK:res.data.comment_list[i].via_cdk,
                                },
                                detail:{
                                    rate:res.data.comment_list[i].rate,
                                    date:res.data.comment_list[i].date,
                                    content:res.data.comment_list[i].content
                                },
                                rate:{
                                    goodCount:res.data.comment_list[i].good_count,
                                    badCount:res.data.comment_list[i].bad_count,
                                },
                                myChoice:{
                                    good:res.data.comment_list[i].good,
                                    bad:res.data.comment_list[i].bad,
                                }
                            }
                        )
                    }
                }
            }).catch( err => {
                console.log(err);
            })
        },
        init:function ()
        {
            console.log('init')
            if(this.curId===0){
                this.comments = this.mostValuedComments;
            }
            else{
                this.comments = this.mostRecentComments;
            }
        },
        apd:function ()
        {
            console.log(this.mostValuedComments.length.toString() + '---------')
            if(this.curId===0)
            {
                for(let i=0;i<this.mostValuedComments.length;i++)
                {
                    console.log('apd')
                    this.comments.push({
                        user:{
                            head:this.mostValuedComments[i].user.head,
                            uid:this.mostValuedComments[i].user.uid,
                            name:this.mostValuedComments[i].user.name,
                            gameNum:this.mostValuedComments[i].user.gameNum,
                            commentNum:this.mostValuedComments[i].user.commentNum,
                            accessTime:this.mostValuedComments[i].user.accessTime,
                            viaCDK:this.mostValuedComments[i].user.viaCDK,
                        },
                        detail:{
                            rate:this.mostValuedComments[i].detail.rate,
                            date:this.mostValuedComments[i].detail.date,
                            content:this.mostValuedComments[i].detail.content,
                        },
                        rate:{
                            goodCount:this.mostValuedComments[i].rate.goodCount,
                            badCount:this.mostValuedComments[i].rate.badCount,
                        },
                        myChoice:{
                            good:this.mostValuedComments[i].myChoice.good,
                            bad:this.mostValuedComments[i].myChoice.bad,
                        }
                    })
                }
            }
            else
            {
                for(let i=0;i<this.mostRecentComments.length;i++)
                {
                    console.log('apd')
                    this.comments.push({
                        user:{
                            head:this.mostRecentComments[i].user.head,
                            uid:this.mostRecentComments[i].user.uid,
                            name:this.mostRecentComments[i].user.name,
                            gameNum:this.mostRecentComments[i].user.gameNum,
                            commentNum:this.mostRecentComments[i].user.commentNum,
                            accessTime:this.mostRecentComments[i].user.accessTime,
                            viaCDK:this.mostRecentComments[i].user.viaCDK,
                        },
                        detail:{
                            rate:this.mostRecentComments[i].detail.rate,
                            date:this.mostRecentComments[i].detail.date,
                            content:this.mostRecentComments[i].detail.content,
                        },
                        rate:{
                            goodCount:this.mostRecentComments[i].rate.goodCount,
                            badCount:this.mostRecentComments[i].rate.badCount,
                        },
                        myChoice:{
                            good:this.mostRecentComments[i].myChoice.good,
                            bad:this.mostRecentComments[i].myChoice.bad,
                        }
                    })
                }
            }
        },
        pic:function ()
        {
            for(let i=0; i<this.comments.length; i++)
            {
                // console.log(this.comments[i].myChoice.good)
                // console.log(this.comments[i].myChoice.bad)
                var pos = this.curId === 0?1:2;
                if(this.comments[i].myChoice.good===true&&this.comments[i].myChoice.bad===true)
                {
                    alert('测评数据出错，不能同时选择good、bad')
                    return;
                }
                if(this.comments[i].myChoice.good===true)
                {
                    document.getElementById(this.getRateID(i+1,'rate-ico-1-',pos)).style.backgroundColor = '#e9e9e9';
                }
                else if(this.comments[i].myChoice.bad===true)
                {
                    document.getElementById(this.getRateID(i+1,'rate-ico-2-',pos)).style.backgroundColor = '#e9e9e9';
                }
                else
                {
                    document.getElementById(this.getRateID(i+1,'rate-ico-1-',pos)).style.backgroundColor = 'transparent';
                    document.getElementById(this.getRateID(i+1,'rate-ico-2-',pos)).style.backgroundColor = 'transparent';
                    console.log('good/bad not choose')
                }

                console.log('comment' + (i+1).toString() + '渲染完毕')
            }
        },
        sendRate:function (gid,uid,cid,cv){
            if(gid===null||uid===null||cid===null||cv===0)
            {
                alert('id 不能为空')
                return;
            }
            console.log('发送' + cid)
            // var self = this;
            this.$axios.post('api/gamedetail/postUserCommentView', {
                game_id: gid,
                user_id:uid,
                creator_id:cid,
                comment_view:cv,
            }).then( res => {
                console.log('add rate' + res.data.result.toString() + res.data.response)
            }).catch( err => {
                console.log(err);
            })
        },
        getID:function (index,pre){
            // console.log(pre + index.toString())
            return pre + index.toString();
        },
        getRateID:function (index,pre,pos){
            // console.log(pre + pos.toString() +'-'+ index.toString())
            return pre + pos.toString() + '-' + index.toString();
        },
        addRate:function (index,type,pos){
            // alert('需要检测是否登录')
            console.log('选择' + (pos===1?'左侧':'右侧') + ' 第' + index.toString() + '条评论 第 ' + type.toString()+  '个按钮')
            let comment = pos===1?this.mostValuedComments:this.mostRecentComments;
            if(type===1)
            {
                //点赞
                if(comment[index].myChoice.good===false)
                {
                    //之前没有点赞但是点了踩
                    if(comment[index].myChoice.bad===true)
                    {
                        comment[index].myChoice.bad = false;
                        document.getElementById(this.getRateID(index+1,'rate-ico-2-',pos)).style.backgroundColor = 'transparent';
                        comment[index].rate.badCount--;
                    }
                    comment[index].myChoice.good=true;
                    document.getElementById(this.getRateID(index+1,'rate-ico-1-',pos)).style.backgroundColor = '#e9e9e9';
                    comment[index].rate.goodCount++;
                }
                else{
                    //取消点赞
                    comment[index].myChoice.good=false;
                    document.getElementById(this.getRateID(index+1,'rate-ico-1-',pos)).style.backgroundColor ='transparent';
                    comment[index].rate.goodCount--;
                }
                this.sendRate(this.game_id,'0000000008',comment[index].user.uid,1)
            }
            else if(type===2)
            {
                if(comment[index].myChoice.bad===false)
                {
                    if(comment[index].myChoice.good===true)
                    {
                        comment[index].myChoice.good=false;
                        document.getElementById(this.getRateID(index+1,'rate-ico-1-',pos)).style.backgroundColor = 'transparent';
                        comment[index].rate.goodCount--;
                    }
                    comment[index].myChoice.bad=true;
                    document.getElementById(this.getRateID(index+1,'rate-ico-2-',pos)).style.backgroundColor = '#e9e9e9';
                    comment[index].rate.badCount++;
                }
                else{
                    comment[index].myChoice.bad = false;
                    document.getElementById(this.getRateID(index+1,'rate-ico-2-',pos)).style.backgroundColor = 'transparent';
                    comment[index].rate.badCount--;
                }
                this.sendRate(this.game_id,'0000000008',comment[index].user.uid,-1)
            }
            else{
                console.log('rate not found err')
            }
        },
    }
}
</script>

<style scoped>
*{
    margin: 0;
    padding: 0;
    box-sizing: border-box;
}
ul, ol{
    list-style: none;
}
a{
    text-decoration: none;
    color: #333;
}
.fl{
    float: left;
}
.fr{
    float: right;
}
.clearbox{
    clear:both;
}
.bb{
    width: 1055px;
    /*min-height:1000px;*/
    margin: auto;
    border-radius: 10px;
    background-color: #c0c0c0;
}
.line4{
    position: relative;
    top:-45px;
    border-radius: 2px;
    border-top: #eeeeee 5px solid;
}
.m-tt{
    font-weight: normal;
    margin-bottom: 10px;
    margin-top: 25px;
    font-size: 20px;
    width: 1005px;
    text-align: left;
}
.m-page{
    width: 1005px;
    min-height: 600px;
    margin: 0 auto;
    overflow:hidden;
    background-color: white;
    border-radius: 20px
}

.comment-filter{
    margin: 50px auto;
    width: 950px;
    height: 110px;
    border-bottom: #aaaaaa 1px solid;
}
.filter-box{
    border: #aaaaaa 1px solid;
    width: 310px;
    border-radius: 5px;
    align-items: center;
    font-size: 14px;
    margin-left: 10px;
    margin-right: 10px;
    margin-bottom: 5px;
}
.filter-title{
    height: 35px;
    line-height: 35px;
    margin-left: 10px;
    width: 290px;
}
.filter-bar{
    height: 35px;
    border:none;
    margin-left: 10px;
    box-shadow:none;
    outline: none;
}
.select-bar{
    border: #aaaaaa 1px solid;
    width: 310px;
    border-radius: 8px;
    align-items: center;
    font-size: 14px;
    margin-left: 10px;
    background-color: white;
    box-shadow: darkgrey 1px 1px 1px 1px;
}
.select-bar li{
    height: 30px;
    font-size: 12px;
    line-height:30px;
    color:#666666;
}
#s1l1:hover,
#s1l2:hover,
#s1l3:hover,
#s2l1:hover,
#s2l2:hover,
#s2l3:hover,
#s3l1:hover,
#s3l2:hover,
#s3l3:hover{
    background-color: #e9e9e9;
}

.tab-bar {
    margin-top: 50px;
    color:#666666;
    font-weight: bolder;
}
.tab-bar .active {
    color: black;
    border-bottom: orange 3px solid;
}
.tab-con{
    margin-top: 20px;
}
.comment-count{
    margin-left: 25px;
}
.comment-tip1{
    display: flex;
    justify-content: space-between;
    align-items: center;
    text-align: center;
    color:#666666;
    width: 700px;
    font-size: 12px;
    margin-left: auto;
    margin-right: auto;
    margin-bottom: 30px;
}
.comment-tip2{
    text-align: center;
    color:#666666;
    width: 800px;
    margin-left: auto;
    margin-right: auto;
}
.line1 {
    height: 0.5px;
    background-color: #666666;
    flex-grow: 0.5;
}

.line2 {
    height:  0.5px;
    background-color: #666666;
    flex-grow:0.5;
}
.comment-tab{
    margin-right: 25px;
}
.m-comment{
    width: 955px;
    margin-left: 25px;
    background-color: #fff;
    background-image: linear-gradient(#333333, #666666);
    margin-bottom: 20px;
    border-radius:15px;
}
.comment-creator{
    margin-top: 80px;
    text-align: center;
    width: 280px;
}
.user-head{
    border:  white 2px solid;
    width: 64px;
    height: 64px;
    margin: auto;
    border-radius:50%;
    overflow:hidden;
}
.user-info{
    margin-top: 10px;
}
.user-info p:first-of-type{
    font-size: 18px;
    font-weight: bold;
}
.user-info p{
    display: block;
    border-bottom: 5px;
    font-size: 12px;
    line-height: 1.5;
    color: white;
}
.comment-rate{
    height: 55px;
}
.line3{
    width: 90%;
    margin: auto;
    border-top: #aaaaaa 1px solid;
}

.rate-name{
    font-size: 18px;
    font-weight: bolder;
    margin-bottom: 5px;
    margin-left: 5px;
}
.rate-count-time{
    font-size: 12px;
    margin-left: 5px;
    margin-top: 30px;
    color:#666666;
}

.m-rate-left{
    margin-top: 15px;
    margin-left: 15px;
}

.m-rate-right{
    margin-top: 20px;
    height: 40px;
}

.m-comment-right{
    width: 670px;
    background-color: #fff;
    margin-top: 3px;
    margin-bottom: 3px;
    margin-right: 3px;
    border-radius:15px
}

.comment-main{
    min-height: 170px;
}
.comment-time{
    margin-top: 15px;
    font-size: 14px;
    color:#666666;
    margin-left: 15px;
    font-weight: bolder;
}

.wrapper {
    display: flex;
    margin-top: 10px;
    margin-right: 10px;
    margin-left: 15px;
    /*margin: auto;*/
    width: 640px;
    overflow: hidden;
    /*border-radius: 8px;*/
    /*padding: 15px ;*/
    /*box-shadow: 20px 20px 60px  #fff,*/
    /*-20px -20px 60px #fff;*/
}
.text {
    font-size: 14px;
    overflow: hidden;
    text-overflow: ellipsis;
    /*text-align: justify;*/
    letter-spacing: normal;
    word-break :break-all;
    word-wrap: break-word;
    position: relative;
    line-height: 1.5;
    max-height: 10.5em;
    transition: .3s max-height;
    color:#666666;
}
.text::before {
    content: '';
    height: calc(100% - 18px);
    float: right;
}
.text::after {
    content: '';
    width: 999vw;
    height: 9999vw;
    position: absolute;
    box-shadow: inset calc(100px - 999vw) calc(30px - 999vw) 0 0  white;
    margin-left: -100px;
}
.btn{
    position: relative;
    float: right;
    clear: both;
    margin-left: 20px;
    font-size: 12px;
    padding: 0 8px;
    background: #3F51B5;
    line-height: 18px;
    border-radius: 4px;
    color:  white;
    cursor: pointer;
}
.btn::after{
    content:'展开'
}
.exp{
    display: none;
}
.exp:checked+.text{
    max-height: none;
}
.exp:checked+.text::after{
    visibility: hidden;
}
.exp:checked+.text .btn::before{
    visibility: hidden;
}
.exp:checked+.text .btn::after{
    content:'收起'
}
.btn::before{
    content: '...';
    position: absolute;
    left: -10px;
    color: #333;
    transform: translateX(-100%)
}

.comment-judge{
}
.comment-judge p{
    font-size: 12px;
}
.rate-ico{
    border: 1px #aaaaaa solid;
    font-size: 12px;
    margin-left: 5px;
    width: 90px;
    height: 26px;
    text-align: center;
    vertical-align: middle;
    border-radius: 13px;
    line-height: 26px;
    display: inline-block;
    color:#666666;
}
/*.rate-ico:hover{*/
/*    background-color: #e9e9e9;*/
/*}*/
.rate-icons{
    margin-bottom: 10px;
    margin-top: 10px;
    margin-left: 20px;
}
.more-comments{
    height: 40px;
    line-height: 40px;
    text-align: center;
    font-size: 14px;
    font-weight: bolder;
    color: #666666;
    background-color: rgb(240, 240, 240 , 50%);
    border-radius: 5px;
    width: 1000px;
    margin: auto auto 50px auto;
}
.end-comments{
    height: 40px;
    line-height: 40px;
    text-align: center;
    font-size: 14px;
    font-weight: bolder;
    color: #666666;
    border-radius: 5px;
    width: 1000px;
    margin: auto auto 20px auto;
}

.more-comments:hover{
    background-color: rgb(240, 240, 240 );
}
.icon-downarrow{
    display:inline-block;
    width:6px;
    height:6px;
    border-left:2px solid black;
    border-bottom:2px solid black;
    transform:rotate(-45deg);
    margin:0 0 4px 6px;
}
</style>
