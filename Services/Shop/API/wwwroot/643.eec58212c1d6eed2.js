"use strict";(self.webpackChunkclient=self.webpackChunkclient||[]).push([[643],{3643:(ue,M,a)=>{a.r(M),a.d(M,{ManagementModule:()=>de});var m=a(6895),g=a(9383),e=a(1571),_=a(2049),l=a(433),Z=a(2521),v=a(835),d=a(5345);function A(o,i){1&o&&(e.TgZ(0,"div",10)(1,"h3"),e._uU(2,"No messages"),e.qZA()())}function y(o,i){if(1&o&&(e.TgZ(0,"div"),e._UZ(1,"img",19),e.TgZ(2,"strong"),e._uU(3),e.ALo(4,"titlecase"),e.qZA()()),2&o){const t=e.oxw().$implicit;e.xp6(1),e.s9C("src",t.recipientPhotoUrl||"./assets/user.png",e.LSH),e.xp6(2),e.Oqu(e.lcZ(4,2,t.recipientUsername))}}function U(o,i){if(1&o&&(e.TgZ(0,"div"),e._UZ(1,"img",19),e.TgZ(2,"strong"),e._uU(3),e.ALo(4,"titlecase"),e.qZA()()),2&o){const t=e.oxw().$implicit;e.xp6(1),e.s9C("src",t.senderPhotoUrl||"./assets/user.png",e.LSH),e.xp6(2),e.Oqu(e.lcZ(4,2,t.senderUsername))}}const q=function(){return{tab:"Messages"}};function J(o,i){if(1&o){const t=e.EpF();e.TgZ(0,"tr",16)(1,"td"),e._uU(2),e.qZA(),e.TgZ(3,"td"),e.YNc(4,y,5,4,"div",17),e.YNc(5,U,5,4,"div",17),e.qZA(),e.TgZ(6,"td"),e._uU(7),e.ALo(8,"timeago"),e.qZA(),e.TgZ(9,"td")(10,"button",18),e.NdJ("click",function(s){return s.stopPropagation()})("click",function(){const r=e.CHM(t).$implicit,c=e.oxw(2);return e.KtG(c.deleteMessage(r.id))}),e._uU(11," Delete "),e.qZA()()()}if(2&o){const t=i.$implicit,n=e.oxw(2);e.s9C("routerLink","Outbox"===n.container?"/members/"+t.recipientUsername:"/members/"+t.senderUsername),e.Q6J("hidden",n.loading)("queryParams",e.DdM(9,q)),e.xp6(2),e.Oqu(t.content),e.xp6(2),e.Q6J("ngIf","Outbox"===n.container),e.xp6(1),e.Q6J("ngIf","Outbox"!==n.container),e.xp6(2),e.Oqu(e.lcZ(8,7,t.messageSent))}}function P(o,i){if(1&o&&(e.TgZ(0,"div",10)(1,"table",11)(2,"thead")(3,"tr")(4,"th",12),e._uU(5,"Message"),e.qZA(),e.TgZ(6,"th",13),e._uU(7,"From / To"),e.qZA(),e.TgZ(8,"th",13),e._uU(9,"Sent / Received"),e.qZA(),e._UZ(10,"th",13),e.qZA()(),e.TgZ(11,"tbody",14),e.YNc(12,J,12,10,"tr",15),e.qZA()()()),2&o){const t=e.oxw();e.xp6(12),e.Q6J("ngForOf",t.messages)}}function O(o,i){if(1&o){const t=e.EpF();e.TgZ(0,"div",20)(1,"pagination",21),e.NdJ("ngModelChange",function(s){e.CHM(t);const r=e.oxw();return e.KtG(r.pagination.currentPage=s)})("pageChanged",function(s){e.CHM(t);const r=e.oxw();return e.KtG(r.pageChanged(s))}),e.qZA()()}if(2&o){const t=e.oxw();e.xp6(1),e.Q6J("boundaryLinks",!0)("totalItems",t.pagination.totalItems)("itemsPerPage",t.pagination.itemsPerPage)("maxSize",10)("ngModel",t.pagination.currentPage)}}let N=(()=>{class o{constructor(t){this.messageService=t,this.container="Unread",this.pageNumber=1,this.pageSize=5,this.loading=!1}ngOnInit(){this.loadMessages()}loadMessages(){this.loading=!0,this.messageService.getMessages(this.pageNumber,this.pageSize,this.container).subscribe({next:t=>{this.messages=t.result.result,this.pagination=t.pagination,this.loading=!1}})}deleteMessage(t){this.messageService.deleteMessage(t).subscribe({next:()=>this.messages?.splice(this.messages.findIndex(n=>n.id===t),1)})}pageChanged(t){this.pageNumber!==t.page&&(this.pageNumber=t.page,this.loadMessages())}}return o.\u0275fac=function(t){return new(t||o)(e.Y36(_.e))},o.\u0275cmp=e.Xpm({type:o,selectors:[["app-messages"]],decls:14,vars:6,consts:[[1,"mb-4","d-flex"],["name","container",1,"btn-group"],["btnRadio","Unread",1,"btn","btn-primary",3,"ngModel","ngModelChange","click"],[1,"fa","fa-envelope"],["btnRadio","Inbox",1,"btn","btn-primary",3,"ngModel","ngModelChange","click"],[1,"fa","fa-envelope-open"],["btnRadio","Outbox",1,"btn","btn-primary",3,"ngModel","ngModelChange","click"],[1,"fa","fa-paper-plane"],["class","row",4,"ngIf"],["class","d-flex justify-content-center",4,"ngIf"],[1,"row"],[1,"table","table-hover",2,"cursor","pointer"],[2,"width","40%"],[2,"width","20%"],[1,"align-middle"],[3,"hidden","routerLink","queryParams",4,"ngFor","ngForOf"],[3,"hidden","routerLink","queryParams"],[4,"ngIf"],[1,"btn","btn-danger",3,"click"],["alt","recipient photo",1,"img-circle","rounded-circle","me-2",3,"src"],[1,"d-flex","justify-content-center"],["previousText","\u2039","nextText","\u203a","firstText","\xab","lastText","\xbb",3,"boundaryLinks","totalItems","itemsPerPage","maxSize","ngModel","ngModelChange","pageChanged"]],template:function(t,n){1&t&&(e.TgZ(0,"div",0)(1,"div",1)(2,"button",2),e.NdJ("ngModelChange",function(r){return n.container=r})("click",function(){return n.loadMessages()}),e._UZ(3,"i",3),e._uU(4," Unread "),e.qZA(),e.TgZ(5,"button",4),e.NdJ("ngModelChange",function(r){return n.container=r})("click",function(){return n.loadMessages()}),e._UZ(6,"i",5),e._uU(7," Inbox "),e.qZA(),e.TgZ(8,"button",6),e.NdJ("ngModelChange",function(r){return n.container=r})("click",function(){return n.loadMessages()}),e._UZ(9,"i",7),e._uU(10," Outbox "),e.qZA()()(),e.YNc(11,A,3,0,"div",8),e.YNc(12,P,13,1,"div",8),e.YNc(13,O,2,5,"div",9)),2&t&&(e.xp6(2),e.Q6J("ngModel",n.container),e.xp6(3),e.Q6J("ngModel",n.container),e.xp6(3),e.Q6J("ngModel",n.container),e.xp6(3),e.Q6J("ngIf",!n.messages||0===n.messages.length),e.xp6(1),e.Q6J("ngIf",n.messages&&n.messages.length>0),e.xp6(1),e.Q6J("ngIf",!n.loading&&n.pagination&&n.messages&&n.messages.length>0))},dependencies:[m.sg,m.O5,g.rH,l.JJ,l.On,Z.Qt,v.lz,m.rS,d.wr],styles:[".img-circle[_ngcontent-%COMP%]{max-height:50px}"]}),o})();var C=a(9105),b=a(5698),h=a(740),T=a(6993),u=a(9241);const S=["messageForm"];function w(o,i){1&o&&(e.TgZ(0,"div"),e._uU(1," No messages yet... say hi by using the message box below "),e.qZA())}function F(o,i){1&o&&(e.TgZ(0,"span",22),e._uU(1," (unread)"),e.qZA())}function k(o,i){if(1&o&&(e.TgZ(0,"span",23),e._uU(1),e.ALo(2,"timeago"),e.qZA()),2&o){const t=e.oxw().$implicit;e.xp6(1),e.hij(" (read ",e.lcZ(2,1,t.dateRead),")")}}function Q(o,i){if(1&o&&(e.TgZ(0,"li")(1,"div")(2,"span",14),e._UZ(3,"img",15),e.qZA(),e.TgZ(4,"div",16)(5,"div",17)(6,"small",18),e._UZ(7,"span",19),e.TgZ(8,"span"),e._uU(9),e.ALo(10,"timeago"),e.qZA(),e.YNc(11,F,2,0,"span",20),e.YNc(12,k,3,3,"span",21),e.qZA()(),e.TgZ(13,"p"),e._uU(14),e.qZA()()()()),2&o){const t=i.$implicit,n=e.oxw(2);e.xp6(3),e.s9C("src",t.senderPhotoUrl||"./assets/user.png",e.LSH),e.xp6(6),e.hij(" ",e.lcZ(10,5,t.messageSent),""),e.xp6(2),e.Q6J("ngIf",!t.dateRead&&t.senderUsername!==n.username),e.xp6(1),e.Q6J("ngIf",t.dateRead&&t.senderUsername!==n.username),e.xp6(2),e.Oqu(t.content)}}function I(o,i){if(1&o&&(e.TgZ(0,"ul",11,12),e.YNc(2,Q,15,7,"li",13),e.ALo(3,"async"),e.qZA()),2&o){const t=e.MAs(1),n=e.oxw();e.Q6J("scrollTop",t.scrollHeight),e.xp6(2),e.Q6J("ngForOf",e.lcZ(3,2,n.messageService.messageThread$))}}let L=(()=>{class o{constructor(t){this.messageService=t,this.messageContent=""}ngOnInit(){}sendMessage(){this.username&&this.messageService.sendMessage(this.username,this.messageContent).then(()=>{this.messageForm?.reset()})}}return o.\u0275fac=function(t){return new(t||o)(e.Y36(_.e))},o.\u0275cmp=e.Xpm({type:o,selectors:[["app-member-messages"]],viewQuery:function(t,n){if(1&t&&e.Gf(S,5),2&t){let s;e.iGM(s=e.CRH())&&(n.messageForm=s.first)}},inputs:{username:"username"},decls:14,vars:8,consts:[[1,"card"],[1,"card-body"],[4,"ngIf"],["style","overflow: scroll; height: 500px","class","chat",3,"scrollTop",4,"ngIf"],[1,"card-footer"],["autocomplete","off",3,"ngSubmit"],["messageForm","ngForm"],[1,"input-group"],["name","messageContent","required","","type","text","placeholder","Send a private message",1,"form-control","input-sm",3,"ngModel","ngModelChange"],[1,"input-group-append"],["type","submit",1,"btn","btn-primary",3,"disabled"],[1,"chat",2,"overflow","scroll","height","500px",3,"scrollTop"],["scrollMe",""],[4,"ngFor","ngForOf"],[1,"chat-img","float-end"],["alt","image of user",1,"rounded-circle",3,"src"],[1,"chat-body"],[1,"header"],[1,"text-muted"],[1,"fa-solid","fa-clock"],["class","text-danger",4,"ngIf"],["class","text-success",4,"ngIf"],[1,"text-danger"],[1,"text-success"]],template:function(t,n){if(1&t&&(e.TgZ(0,"div",0)(1,"div",1),e.YNc(2,w,2,0,"div",2),e.ALo(3,"async"),e.YNc(4,I,4,4,"ul",3),e.ALo(5,"async"),e.qZA(),e.TgZ(6,"div",4)(7,"form",5,6),e.NdJ("ngSubmit",function(){return n.sendMessage()}),e.TgZ(9,"div",7)(10,"input",8),e.NdJ("ngModelChange",function(r){return n.messageContent=r}),e.qZA(),e.TgZ(11,"div",9)(12,"button",10),e._uU(13,"Send"),e.qZA()()()()()()),2&t){const s=e.MAs(8);let r;e.xp6(2),e.Q6J("ngIf",0===(null==(r=e.lcZ(3,4,n.messageService.messageThread$))?null:r.length)),e.xp6(2),e.Q6J("ngIf",e.lcZ(5,6,n.messageService.messageThread$).length>0),e.xp6(6),e.Q6J("ngModel",n.messageContent),e.xp6(2),e.Q6J("disabled",!s.valid)}},dependencies:[m.sg,m.O5,l._Y,l.Fj,l.JJ,l.JL,l.Q7,l.On,l.F,m.Ov,d.wr],styles:[".card[_ngcontent-%COMP%]{border:none}.chat[_ngcontent-%COMP%]{list-style:none;margin:0;padding:0}.chat[_ngcontent-%COMP%]   li[_ngcontent-%COMP%]{margin-bottom:10px;padding-bottom:10px;border-bottom:1px dotted #b3a9a9}.rounded-circle[_ngcontent-%COMP%]{max-height:50px}"],changeDetection:0}),o})();const E=["memberTabs"];function Y(o,i){1&o&&(e.TgZ(0,"div",19),e._UZ(1,"i",20),e._uU(2," Online now "),e.qZA())}let H=(()=>{class o{constructor(t,n,s,r,c){this.accountService=t,this.route=n,this.messageService=s,this.presenceService=r,this.router=c,this.member={},this.galleryOptions=[],this.galleryImages=[],this.messages=[],this.accountService.currentUser$.pipe((0,b.q)(1)).subscribe({next:x=>{x&&(this.user=x)}}),this.router.routeReuseStrategy.shouldReuseRoute=()=>!1}ngOnInit(){this.route.data.subscribe({next:t=>this.member=t.member}),this.route.queryParams.subscribe({next:t=>{t.tab&&this.selectTab(t.tab)}}),this.galleryOptions=[{width:"500px",height:"500px",imagePercent:100,thumbnailsColumns:4,imageAnimation:C.zw.Slide,imageSwipe:!0,preview:!1}],this.galleryImages=this.getImages()}ngOnDestroy(){this.messageService.stopHubConnection()}getImages(){if(!this.member)return[];const t=[];for(const n of this.member.photos)t.push({small:n.url,medium:n.url,big:n.url});return t}selectTab(t){this.memberTabs&&(this.memberTabs.tabs.find(n=>n.heading===t).active=!0)}loadMessages(){this.member&&this.messageService.getMessageThread(this.member.userName).subscribe({next:t=>this.messages=t})}onTabActivated(t){this.activeTab=t,"Messages"===this.activeTab.heading&&this.user?this.messageService.createHubConnection(this.user,this.member.userName):this.messageService.stopHubConnection()}}return o.\u0275fac=function(t){return new(t||o)(e.Y36(h.B),e.Y36(g.gz),e.Y36(_.e),e.Y36(T.Q),e.Y36(g.F0))},o.\u0275cmp=e.Xpm({type:o,selectors:[["app-member-detail"]],viewQuery:function(t,n){if(1&t&&e.Gf(E,7),2&t){let s;e.iGM(s=e.CRH())&&(n.memberTabs=s.first)}},decls:56,vars:22,consts:[[1,"row"],[1,"col-4"],[1,"card"],[1,"card-img-top","img-thumbnail",3,"src","alt"],[1,"card-body"],["class","mb-2",4,"ngIf"],[1,"card-footer"],[1,"btn-group","d-flex"],[1,"btn","btn-primary"],[1,"btn","btn-success",3,"click"],[1,"col-8"],[1,"member-tabset"],["memberTabs",""],[3,"heading","selectTab"],["heading","Interests",3,"selectTab"],["heading","Photos",3,"selectTab"],[1,"ngx-gallery",3,"options","images"],["heading","Messages",3,"selectTab"],[3,"username"],[1,"mb-2"],[1,"class","fa","fa-user-circle","text-success"]],template:function(t,n){if(1&t&&(e.TgZ(0,"div",0)(1,"div",1)(2,"div",2),e._UZ(3,"img",3),e.TgZ(4,"div",4),e.YNc(5,Y,3,0,"div",5),e.ALo(6,"async"),e.TgZ(7,"div")(8,"strong"),e._uU(9,"Location:"),e.qZA(),e.TgZ(10,"p"),e._uU(11),e.qZA()(),e.TgZ(12,"div")(13,"strong"),e._uU(14,"Age:"),e.qZA(),e.TgZ(15,"p"),e._uU(16),e.qZA()(),e.TgZ(17,"div")(18,"strong"),e._uU(19,"Last active:"),e.qZA(),e.TgZ(20,"p"),e._uU(21),e.ALo(22,"timeago"),e.qZA()(),e.TgZ(23,"div")(24,"strong"),e._uU(25,"Member since:"),e.qZA(),e.TgZ(26,"p"),e._uU(27),e.ALo(28,"date"),e.qZA()()(),e.TgZ(29,"div",6)(30,"div",7)(31,"button",8),e._uU(32,"Like"),e.qZA(),e.TgZ(33,"button",9),e.NdJ("click",function(){return n.selectTab("Messages")}),e._uU(34,"Messages"),e.qZA()()()()(),e.TgZ(35,"div",10)(36,"tabset",11,12)(38,"tab",13),e.NdJ("selectTab",function(r){return n.onTabActivated(r)}),e.TgZ(39,"h4"),e._uU(40,"Description"),e.qZA(),e.TgZ(41,"p"),e._uU(42),e.qZA(),e.TgZ(43,"h4"),e._uU(44,"Looking for"),e.qZA(),e.TgZ(45,"p"),e._uU(46),e.qZA()(),e.TgZ(47,"tab",14),e.NdJ("selectTab",function(r){return n.onTabActivated(r)}),e.TgZ(48,"h4"),e._uU(49,"Interests"),e.qZA(),e.TgZ(50,"p"),e._uU(51),e.qZA()(),e.TgZ(52,"tab",15),e.NdJ("selectTab",function(r){return n.onTabActivated(r)}),e._UZ(53,"ngx-gallery",16),e.qZA(),e.TgZ(54,"tab",17),e.NdJ("selectTab",function(r){return n.onTabActivated(r)}),e._UZ(55,"app-member-messages",18),e.qZA()()()()),2&t){let s;e.xp6(3),e.s9C("src",n.member.photoUrl||"./assets/user.png",e.LSH),e.s9C("alt",n.member.firstName),e.xp6(2),e.Q6J("ngIf",null==(s=e.lcZ(6,15,n.presenceService.onlineUsers$))?null:s.includes(n.member.userName)),e.xp6(6),e.AsE("",n.member.firstName,", ",n.member.firstName,""),e.xp6(5),e.Oqu(n.member.firstName),e.xp6(5),e.Oqu(e.lcZ(22,17,n.member.lastActive)),e.xp6(6),e.Oqu(e.xi3(28,19,n.member.created,"dd MMM yyyy")),e.xp6(11),e.MGl("heading","About ",n.member.firstName,""),e.xp6(4),e.Oqu(n.member.firstName),e.xp6(4),e.Oqu(n.member.firstName),e.xp6(5),e.Oqu(n.member.firstName),e.xp6(2),e.Q6J("options",n.galleryOptions)("images",n.galleryImages),e.xp6(2),e.Q6J("username",n.member.userName)}},dependencies:[m.O5,u.wW,u.AH,C.g$,L,m.Ov,m.uU,d.wr],styles:[".img-thumbnail[_ngcontent-%COMP%]{margin:25px;width:85%;height:85%}.card-body[_ngcontent-%COMP%]{padding:0 25px}.card-footer[_ngcontent-%COMP%]{padding:10px 15px;background-color:#fff;border-top:none}.ngx-gallery[_ngcontent-%COMP%]{display:inline-block;margin-bottom:20px}"]}),o})();var p=a(5218);let B=(()=>{class o{constructor(t){this.memberService=t}resolve(t){return this.memberService.getMember(+t.paramMap.get("userId"))}}return o.\u0275fac=function(t){return new(t||o)(e.LFG(p.Z))},o.\u0275prov=e.Yz7({token:o,factory:o.\u0275fac,providedIn:"root"}),o})();var G=a(7185),f=a(1129),D=a(2340);function R(o,i){if(1&o){const t=e.EpF();e.TgZ(0,"div",13),e._UZ(1,"img",14),e.TgZ(2,"div")(3,"button",15),e.NdJ("click",function(){const r=e.CHM(t).$implicit,c=e.oxw();return e.KtG(c.setMainPhoto(r))}),e._uU(4," Main "),e.qZA(),e.TgZ(5,"button",16),e.NdJ("click",function(){const r=e.CHM(t).$implicit,c=e.oxw();return e.KtG(c.deletePhoto(r.id))}),e._UZ(6,"i",17),e.qZA()()()}if(2&o){const t=i.$implicit;e.xp6(1),e.s9C("src",t.url,e.LSH),e.s9C("alt",t.url),e.xp6(2),e.Q6J("disabled",t.isMain)("ngClass",t.isMain?"btn-success active":"btn-outline-success"),e.xp6(2),e.Q6J("disabled",t.isMain)}}function z(o,i){if(1&o&&(e.TgZ(0,"td"),e._uU(1),e.ALo(2,"number"),e.qZA()),2&o){const t=e.oxw().$implicit;e.xp6(1),e.hij("",e.xi3(2,1,(null==t||null==t.file?null:t.file.size)/1024/1024,".4")," MB")}}function $(o,i){if(1&o&&(e.TgZ(0,"tr")(1,"td"),e._uU(2),e.qZA(),e.YNc(3,z,3,4,"td",30),e.qZA()),2&o){const t=i.$implicit,n=e.oxw(2);e.xp6(2),e.hij(" ",null==t||null==t.file?null:t.file.name," "),e.xp6(1),e.Q6J("ngIf",n.uploader.options.isHTML5)}}const K=function(o){return{width:o}};function j(o,i){if(1&o){const t=e.EpF();e.TgZ(0,"div",18)(1,"h6"),e._uU(2),e.qZA(),e._UZ(3,"p"),e.TgZ(4,"table",19)(5,"thead")(6,"tr")(7,"th",20),e._uU(8,"Name"),e.qZA(),e.TgZ(9,"th"),e._uU(10,"Size"),e.qZA()()(),e.TgZ(11,"tbody"),e.YNc(12,$,4,2,"tr",21),e.qZA()(),e.TgZ(13,"div"),e._uU(14," Queue progress: "),e.TgZ(15,"div",22),e._UZ(16,"div",23),e.qZA()(),e.TgZ(17,"div",24)(18,"button",25),e.NdJ("click",function(){e.CHM(t);const s=e.oxw();return e.KtG(s.uploader.uploadAll())}),e._UZ(19,"span",26),e._uU(20," Upload all "),e.qZA(),e.TgZ(21,"button",27),e.NdJ("click",function(){e.CHM(t);const s=e.oxw();return e.KtG(s.uploader.cancelAll())}),e._UZ(22,"span",28),e._uU(23," Cancel all "),e.qZA(),e.TgZ(24,"button",29),e.NdJ("click",function(){e.CHM(t);const s=e.oxw();return e.KtG(s.uploader.clearQueue())}),e._UZ(25,"span",17),e._uU(26," Remove all "),e.qZA()()()}if(2&o){const t=e.oxw();e.xp6(2),e.hij("Upload queue, Queue length: ",null==t.uploader||null==t.uploader.queue?null:t.uploader.queue.length,""),e.xp6(10),e.Q6J("ngForOf",t.uploader.queue),e.xp6(4),e.Q6J("ngStyle",e.VKq(6,K,t.uploader.progress+"%")),e.xp6(2),e.Q6J("disabled",!t.uploader.getNotUploadedItems().length),e.xp6(3),e.Q6J("disabled",!t.uploader.isUploading),e.xp6(3),e.Q6J("disabled",!t.uploader.queue.length)}}const X=function(o){return{"nv-file-over":o}};let V=(()=>{class o{constructor(t,n){this.accountService=t,this.memberService=n,this.hasBaseDropzoneOver=!1,this.baseUrl=D.N.apiUrl,this.accountService.currentUser$.pipe((0,b.q)(1)).subscribe(s=>this.user=s)}ngOnInit(){this.initializeUploader()}fileOverBase(t){this.hasBaseDropzoneOver=t}setMainPhoto(t){this.memberService.setMainPhoto(t.id).subscribe(()=>{this.user.photoUrl=t.url,this.accountService.setCurrentUser(this.user),this.member.photoUrl=t.url,this.member.photos.forEach(n=>{n.isMain&&(n.isMain=!1),n.id===t.id&&(n.isMain=!0)})})}deletePhoto(t){this.memberService.deletePhoto(t).subscribe(()=>{this.member.photos=this.member.photos.filter(n=>n.id!==t)})}initializeUploader(){this.uploader=new f.bA({url:this.baseUrl+"users/add-photo",authToken:"Bearer "+this.user.token,isHTML5:!0,allowedFileType:["image"],removeAfterUpload:!0,autoUpload:!1,maxFileSize:10485760}),this.uploader.onAfterAddingFile=t=>{t.withCredentials=!1},this.uploader.onSuccessItem=(t,n,s,r)=>{if(n){const c=JSON.parse(n);this.member.photos.push(c.result)}}}}return o.\u0275fac=function(t){return new(t||o)(e.Y36(h.B),e.Y36(p.Z))},o.\u0275cmp=e.Xpm({type:o,selectors:[["app-photo-editor"]],inputs:{member:"member"},decls:20,vars:8,consts:[[1,"row"],["class","col-2 text-center",4,"ngFor","ngForOf"],[1,"row","mt-4"],[1,"col-2"],[1,"h6"],["ng2FileDrop","",1,"card","bg-faded","p-3","text-center","mb-3","my-drop-zone",3,"ngClass","uploader","fileOver"],[1,"fa","fa-upload","fa-3x"],["type","file","id","selectedFiles","ng2FileSelect","","multiple","",2,"display","none",3,"uploader"],["type","button","onclick","document.getElementById('selectedFiles').click();",1,"btn","btn-primary","btn-sm"],[1,"mt-2"],["type","file","id","selectedFile","ng2FileSelect","","multiple","",2,"display","none",3,"uploader"],["type","button","onclick","document.getElementById('selectedFile').click();",1,"btn","btn-primary","btn-sm"],["class","col-10 ps-5",4,"ngIf"],[1,"col-2","text-center"],[1,"img-thumbnail","p-1",3,"src","alt"],[1,"btn","btn-sm","me-1",3,"disabled","ngClass","click"],[1,"btn","btn-sm","btn-danger",3,"disabled","click"],[1,"fa","fa-trash"],[1,"col-10","ps-5"],["aria-describedby","sdf",1,"table"],[2,"width","50%"],[4,"ngFor","ngForOf"],[1,"progress"],["role","progressbar",1,"progress-bar",3,"ngStyle"],[1,"mt-3"],["type","button",1,"btn","btn-success","btn-sm","me-2",3,"disabled","click"],[1,"fa","fa-upload"],["type","button",1,"btn","btn-warning","btn-sm","me-2",3,"disabled","click"],[1,"fa","fa-ban"],["type","button",1,"btn","btn-danger","btn-sm","me-2",3,"disabled","click"],[4,"ngIf"]],template:function(t,n){1&t&&(e.TgZ(0,"div",0),e.YNc(1,R,7,5,"div",1),e.qZA(),e.TgZ(2,"div",2)(3,"div",3)(4,"p",4),e._uU(5,"Add Photos"),e.qZA(),e.TgZ(6,"div",5),e.NdJ("fileOver",function(r){return n.fileOverBase(r)}),e._UZ(7,"i",6),e._uU(8," Drop photos here "),e.qZA(),e.TgZ(9,"div"),e._uU(10,"Multiple"),e.qZA(),e._UZ(11,"input",7),e.TgZ(12,"button",8),e._uU(13," Choose Files "),e.qZA(),e.TgZ(14,"div",9),e._uU(15,"Single"),e.qZA(),e._UZ(16,"input",10),e.TgZ(17,"button",11),e._uU(18," Choose File "),e.qZA()(),e.YNc(19,j,27,8,"div",12),e.qZA()),2&t&&(e.xp6(1),e.Q6J("ngForOf",n.member.photos),e.xp6(5),e.Q6J("ngClass",e.VKq(6,X,n.hasBaseDropzoneOver))("uploader",n.uploader),e.xp6(5),e.Q6J("uploader",n.uploader),e.xp6(5),e.Q6J("uploader",n.uploader),e.xp6(3),e.Q6J("ngIf",null==n.uploader||null==n.uploader.queue?null:n.uploader.queue.length))},dependencies:[m.mk,m.sg,m.O5,m.PC,f.GN,f.C6,m.JJ],styles:["img.img-thumbnail[_ngcontent-%COMP%]{height:100px;min-width:100px!important;margin-bottom:2px}.nv-file-over[_ngcontent-%COMP%]{border:dotted 3px red}input[type=file][_ngcontent-%COMP%]{color:transparent}"]}),o})();var W=a(1094);const ee=["editForm"];function te(o,i){1&o&&(e.TgZ(0,"div",26)(1,"strong"),e._uU(2,"Information: "),e.qZA(),e._uU(3," You have made changes. Any unsaved changes will be lost "),e.qZA())}function ne(o,i){if(1&o){const t=e.EpF();e.TgZ(0,"div",1)(1,"div",2)(2,"h1"),e._uU(3,"Your profile"),e.qZA()(),e.TgZ(4,"div",3),e.YNc(5,te,4,0,"div",4),e.qZA(),e.TgZ(6,"div",5)(7,"div",6),e._UZ(8,"img",7),e.TgZ(9,"div",8)(10,"div")(11,"strong"),e._uU(12,"Phone Number:"),e.qZA(),e.TgZ(13,"p"),e._uU(14),e.ALo(15,"mask"),e.qZA()(),e.TgZ(16,"div")(17,"strong"),e._uU(18,"Last Active:"),e.qZA(),e.TgZ(19,"p"),e._uU(20),e.ALo(21,"timeago"),e.qZA()(),e.TgZ(22,"div")(23,"strong"),e._uU(24,"Member since:"),e.qZA(),e.TgZ(25,"p"),e._uU(26),e.ALo(27,"date"),e.qZA()()(),e.TgZ(28,"div",9)(29,"button",10),e._uU(30,"Save Changes"),e.qZA()()()(),e.TgZ(31,"div",11)(32,"tabset",12)(33,"tab",13)(34,"form",14,15),e.NdJ("ngSubmit",function(){e.CHM(t);const s=e.oxw();return e.KtG(s.updateMember())}),e.TgZ(36,"h4"),e._uU(37,"Description"),e.qZA(),e.TgZ(38,"textarea",16),e.NdJ("ngModelChange",function(s){e.CHM(t);const r=e.oxw();return e.KtG(r.member.introduction=s)}),e.qZA(),e.TgZ(39,"h4",17),e._uU(40,"Looking for"),e.qZA(),e.TgZ(41,"textarea",18),e.NdJ("ngModelChange",function(s){e.CHM(t);const r=e.oxw();return e.KtG(r.member.lookingFor=s)}),e.qZA(),e.TgZ(42,"h4",17),e._uU(43,"Interests"),e.qZA(),e.TgZ(44,"textarea",19),e.NdJ("ngModelChange",function(s){e.CHM(t);const r=e.oxw();return e.KtG(r.member.interests=s)}),e.qZA(),e.TgZ(45,"h4",17),e._uU(46,"Location Details:"),e.qZA(),e.TgZ(47,"div",20)(48,"label",21),e._uU(49,"City: "),e.qZA(),e.TgZ(50,"input",22),e.NdJ("ngModelChange",function(s){e.CHM(t);const r=e.oxw();return e.KtG(r.member.city=s)}),e.qZA(),e.TgZ(51,"label",21),e._uU(52,"Country: "),e.qZA(),e.TgZ(53,"input",23),e.NdJ("ngModelChange",function(s){e.CHM(t);const r=e.oxw();return e.KtG(r.member.country=s)}),e.qZA()()()(),e.TgZ(54,"tab",24),e._UZ(55,"app-photo-editor",25),e.qZA()()()()}if(2&o){const t=e.MAs(35),n=e.oxw();e.xp6(5),e.Q6J("ngIf",t.dirty),e.xp6(3),e.s9C("src",n.member.photoUrl||"./assets/user.png",e.LSH),e.s9C("alt",n.member.username),e.xp6(6),e.Oqu(e.xi3(15,14,n.member.phoneNumber,"(000) 000 00 00")),e.xp6(6),e.Oqu(e.lcZ(21,17,n.member.lastActive)),e.xp6(6),e.Oqu(e.xi3(27,19,n.member.created,"dd MMM yyy")),e.xp6(3),e.Q6J("disabled",!t.dirty),e.xp6(4),e.MGl("heading","About ",n.member.knownAs,""),e.xp6(5),e.Q6J("ngModel",n.member.introduction),e.xp6(3),e.Q6J("ngModel",n.member.lookingFor),e.xp6(3),e.Q6J("ngModel",n.member.interests),e.xp6(6),e.Q6J("ngModel",n.member.city),e.xp6(3),e.Q6J("ngModel",n.member.country),e.xp6(2),e.Q6J("member",n.member)}}let oe=(()=>{class o{constructor(t,n,s){this.accountService=t,this.memberService=n,this.toastr=s,this.accountService.currentUser$.pipe((0,b.q)(1)).subscribe(r=>this.user=r)}unloadNotification(t){this.editForm.dirty&&(t.returnValue=!0)}ngOnInit(){this.loadMember()}loadMember(){this.memberService.getMember(this.user.userId).subscribe(t=>{this.member=t})}updateMember(){this.memberService.updateMember(this.member).subscribe(()=>{this.toastr.success("Profile updated successfully"),this.editForm.reset(this.member)})}}return o.\u0275fac=function(t){return new(t||o)(e.Y36(h.B),e.Y36(p.Z),e.Y36(G._W))},o.\u0275cmp=e.Xpm({type:o,selectors:[["app-member-edit"]],viewQuery:function(t,n){if(1&t&&e.Gf(ee,5),2&t){let s;e.iGM(s=e.CRH())&&(n.editForm=s.first)}},hostBindings:function(t,n){1&t&&e.NdJ("beforeunload",function(r){return n.unloadNotification(r)},!1,e.Jf7)},decls:1,vars:1,consts:[["class","row",4,"ngIf"],[1,"row"],[1,"col-4"],[1,"col-8"],["class","alert alert-info",4,"ngIf"],[1,"col-3"],[1,"card"],[1,"card-img-top","img-thumbnail",3,"src","alt"],[1,"card-body"],[1,"card-footer","d-grid"],["form","editForm","type","submit",1,"btn","btn-success",3,"disabled"],[1,"col-9"],[1,"member-tabset"],[3,"heading"],["id","editForm",3,"ngSubmit"],["editForm","ngForm"],["name","introduction","rows","6",1,"form-control",3,"ngModel","ngModelChange"],[1,"mt-2"],["name","lookingFor","rows","6",1,"form-control",3,"ngModel","ngModelChange"],["name","interests","rows","6",1,"form-control",3,"ngModel","ngModelChange"],[1,"form-inline"],["for","city"],["type","text","name","city",1,"form-control","mx-2",3,"ngModel","ngModelChange"],["type","text","name","country",1,"form-control","mx-2",3,"ngModel","ngModelChange"],["heading","Edit Photos"],[3,"member"],[1,"alert","alert-info"]],template:function(t,n){1&t&&e.YNc(0,ne,56,22,"div",0),2&t&&e.Q6J("ngIf",n.member)},dependencies:[m.O5,u.wW,u.AH,l._Y,l.Fj,l.JJ,l.JL,l.On,l.F,V,m.uU,W.Iq,d.wr],styles:[".img-thumbnail[_ngcontent-%COMP%]{margin:25px;width:85%;height:85%}.card-body[_ngcontent-%COMP%]{padding:0 25px}.card-footer[_ngcontent-%COMP%]{padding:10px 15px;background-color:#fff;border-top:none}"]}),o})();const se=function(){return{tab:"Messages"}};function ie(o,i){if(1&o){const t=e.EpF();e.TgZ(0,"div",1)(1,"div",2),e._UZ(2,"img",3),e.TgZ(3,"ul",4)(4,"li",5)(5,"button",6),e._UZ(6,"i",7),e.qZA()(),e.TgZ(7,"li",5)(8,"button",8),e.NdJ("click",function(){e.CHM(t);const s=e.oxw();return e.KtG(s.addLike(s.member))}),e._UZ(9,"i",9),e.qZA()(),e.TgZ(10,"li",5)(11,"button",10),e._UZ(12,"i",11),e.qZA()()()(),e.TgZ(13,"div",12)(14,"h6",13)(15,"span"),e.ALo(16,"async"),e._UZ(17,"i",14),e.qZA(),e._uU(18),e.qZA(),e.TgZ(19,"p",15),e._uU(20),e.qZA()()()}if(2&o){const t=e.oxw();let n;e.xp6(2),e.s9C("src",t.member.photoUrl||"./assets/user.png",e.LSH),e.s9C("alt",t.member.knownAs),e.xp6(3),e.MGl("routerLink","/uyeler/",t.member.id,""),e.xp6(6),e.MGl("routerLink","/uyeler/",t.member.id,""),e.Q6J("queryParams",e.DdM(12,se)),e.xp6(4),e.ekj("is-online",null==(n=e.lcZ(16,10,t.presenceService.onlineUsers$))?null:n.includes(t.member.userName)),e.xp6(3),e.AsE(" ",t.member.firstName,", ",t.member.userName," "),e.xp6(2),e.Oqu(t.member.city)}}let re=(()=>{class o{constructor(t){this.presenceService=t}ngOnInit(){}}return o.\u0275fac=function(t){return new(t||o)(e.Y36(T.Q))},o.\u0275cmp=e.Xpm({type:o,selectors:[["app-member-card"]],inputs:{member:"member"},decls:1,vars:1,consts:[["class","card mb-4",4,"ngIf"],[1,"card","mb-4"],[1,"card-img-wrapper"],[1,"card-img-top",3,"src","alt"],[1,"list-inline","member-icons","animate","text-center"],[1,"list-inline-item"],[1,"btn","btn-primary",3,"routerLink"],[1,"fa","fa-user"],[1,"btn","btn-primary",3,"click"],[1,"fa","fa-heart"],[1,"btn","btn-primary",3,"routerLink","queryParams"],[1,"fa","fa-envelope"],[1,"card-body","p-1"],[1,"card-title","text-center","mb-1"],[1,"fa","fa-user","me-2"],[1,"card-text","text-muted","text-center"]],template:function(t,n){1&t&&e.YNc(0,ie,21,13,"div",0),2&t&&e.Q6J("ngIf",n.member)},dependencies:[m.O5,g.rH,m.Ov],styles:[".card[_ngcontent-%COMP%]:hover   img[_ngcontent-%COMP%]{transform:scale(1.2);transition-duration:.5s;transition-timing-function:ease-out;opacity:.7}.card[_ngcontent-%COMP%]   img[_ngcontent-%COMP%]{transform:scale(1);transition-duration:.5s;transition-timing-function:ease-out}.card-img-wrapper[_ngcontent-%COMP%]{overflow:hidden;position:relative}.member-icons[_ngcontent-%COMP%]{position:absolute;bottom:-30%;left:0;right:0;margin-left:auto;margin-right:auto;opacity:0}.card-img-wrapper[_ngcontent-%COMP%]:hover   .member-icons[_ngcontent-%COMP%]{bottom:0;opacity:1}.animate[_ngcontent-%COMP%]{transition:all .3s ease-in-out}@keyframes _ngcontent-%COMP%_fa-blink{0%{opacity:1}to{opacity:.4}}.is-online[_ngcontent-%COMP%]{animation:_ngcontent-%COMP%_fa-blink 1.5s linear infinite;color:#01bd2a}"]}),o})();function ae(o,i){if(1&o&&(e.TgZ(0,"div",11),e._UZ(1,"app-member-card",12),e.qZA()),2&o){const t=i.$implicit;e.xp6(1),e.Q6J("member",t)}}function me(o,i){if(1&o){const t=e.EpF();e.TgZ(0,"div",13)(1,"pagination",14),e.NdJ("ngModelChange",function(s){e.CHM(t);const r=e.oxw();return e.KtG(r.pagination.currentPage=s)})("pageChanged",function(s){e.CHM(t);const r=e.oxw();return e.KtG(r.pageChanged(s))}),e.qZA()()}if(2&o){const t=e.oxw();e.xp6(1),e.Q6J("boundaryLinks",!0)("totalItems",t.pagination.totalItems)("itemsPerPage",t.pagination.itemsPerPage)("ngModel",t.pagination.currentPage)}}const le=[{path:"",component:(()=>{class o{constructor(t){this.memberService=t,this.userParams=this.memberService.getUserParams()}ngOnInit(){this.loadMembers()}loadMembers(){this.memberService.setUserParams(this.userParams),this.memberService.getMembers(this.userParams).subscribe(t=>{this.members=t.result.result,this.pagination=t.pagination})}resetFilters(){this.userParams=this.memberService.resetUserParams(),this.loadMembers()}pageChanged(t){this.userParams.pageNumber=t.page,this.memberService.setUserParams(this.userParams),this.loadMembers()}}return o.\u0275fac=function(t){return new(t||o)(e.Y36(p.Z))},o.\u0275cmp=e.Xpm({type:o,selectors:[["app-member-list"]],decls:16,vars:5,consts:[[1,"text-center","mt-3"],[1,"container","mt-3"],["autocomplete","off",1,"form-inline","mb-3",3,"ngSubmit"],["form","ngForm"],[1,"row"],[1,"col-auto"],[1,"btn-group","float-right"],["type","button","name","orderBy","btnRadio","lastActive",1,"btn","btn-primary",3,"ngModel","click","ngModelChange"],["type","button","name","orderBy","btnRadio","created",1,"btn","btn-primary",3,"ngModel","click","ngModelChange"],["class","col-2",4,"ngFor","ngForOf"],["class","d-flex justify-content-center",4,"ngIf"],[1,"col-2"],[3,"member"],[1,"d-flex","justify-content-center"],["previousText","\u2039","nextText","\u203a","firstText","\xab","lastText","\xbb",3,"boundaryLinks","totalItems","itemsPerPage","ngModel","ngModelChange","pageChanged"]],template:function(t,n){1&t&&(e.TgZ(0,"div",0)(1,"h2"),e._uU(2),e.qZA()(),e.TgZ(3,"div",1)(4,"form",2,3),e.NdJ("ngSubmit",function(){return n.loadMembers()}),e.TgZ(6,"div",4)(7,"div",5)(8,"div",6)(9,"button",7),e.NdJ("click",function(){return n.loadMembers()})("ngModelChange",function(r){return n.userParams.orderBy=r}),e._uU(10," Last Active "),e.qZA(),e.TgZ(11,"button",8),e.NdJ("click",function(){return n.loadMembers()})("ngModelChange",function(r){return n.userParams.orderBy=r}),e._uU(12," Newest Members "),e.qZA()()()()()(),e.TgZ(13,"div",4),e.YNc(14,ae,2,1,"div",9),e.qZA(),e.YNc(15,me,2,4,"div",10)),2&t&&(e.xp6(2),e.hij("Your matches - ",null==n.pagination?null:n.pagination.totalItems," found"),e.xp6(7),e.Q6J("ngModel",n.userParams.orderBy),e.xp6(2),e.Q6J("ngModel",n.userParams.orderBy),e.xp6(3),e.Q6J("ngForOf",n.members),e.xp6(1),e.Q6J("ngIf",n.pagination))},dependencies:[m.sg,m.O5,Z.Qt,v.lz,l._Y,l.JJ,l.JL,l.On,l.F,re]}),o})()},{path:"messages",component:N},{path:"edit",component:oe},{path:":userId",component:H,resolve:{member:B}}];let ce=(()=>{class o{}return o.\u0275fac=function(t){return new(t||o)},o.\u0275mod=e.oAB({type:o}),o.\u0275inj=e.cJS({imports:[g.Bz.forChild(le),g.Bz]}),o})();var ge=a(9066);let de=(()=>{class o{}return o.\u0275fac=function(t){return new(t||o)},o.\u0275mod=e.oAB({type:o}),o.\u0275inj=e.cJS({imports:[m.ez,ge.m,ce,l.u5]}),o})()}}]);