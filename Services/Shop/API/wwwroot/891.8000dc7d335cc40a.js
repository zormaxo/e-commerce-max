"use strict";(self.webpackChunkclient=self.webpackChunkclient||[]).push([[891],{3891:(T,a,c)=>{c.r(a),c.d(a,{SearchResultModule:()=>Z});var i=c(6895),l=c(6099),t=c(1571),h=c(6674),r=c(9383);function p(e,n){if(1&e&&(t.TgZ(0,"div",4)(1,"span",5),t._uU(2),t.qZA(),t._uU(3," result are found by searching "),t.TgZ(4,"span",5),t._uU(5),t.qZA()()),2&e){const o=t.oxw();t.xp6(2),t.Oqu(o.totalCount),t.xp6(3),t.Oqu(o.searcText)}}const d=function(e,n){return["/",e,n]},u=function(e){return{searchTerm:e}};function m(e,n){if(1&e&&(t.TgZ(0,"a",12),t._uU(1),t.qZA()),2&e){const o=t.oxw().$implicit,s=t.oxw(2).$implicit,R=t.oxw();t.Q6J("routerLink",t.WLB(4,d,s.url,o.url))("state",t.VKq(7,u,R.shopService.shopParams.search)),t.xp6(1),t.AsE(" ",o.name," (",o.count,") ")}}function g(e,n){if(1&e&&(t.TgZ(0,"div",14),t.YNc(1,m,2,9,"a",15),t.qZA()),2&e){const o=n.$implicit;t.xp6(1),t.Q6J("ngIf",o.count)}}const f=function(e){return["/",e]};function v(e,n){if(1&e&&(t.TgZ(0,"div",7)(1,"div",8)(2,"div",9),t._UZ(3,"img",10),t.TgZ(4,"div",11)(5,"a",12),t._uU(6),t.TgZ(7,"div")(8,"span",4),t._uU(9),t.qZA(),t._uU(10," ilan "),t.qZA()(),t._UZ(11,"hr"),t.YNc(12,g,2,1,"div",13),t.qZA()()()()),2&e){const o=t.oxw().$implicit,s=t.oxw();t.xp6(3),t.s9C("src","../assets/icons/"+o.url+".svg",t.LSH),t.xp6(2),t.Q6J("routerLink",t.VKq(6,f,o.url))("state",t.VKq(8,u,s.shopService.shopParams.search)),t.xp6(1),t.hij(" ",o.name," "),t.xp6(3),t.Oqu(o.count),t.xp6(3),t.Q6J("ngForOf",o.childCategories)}}function x(e,n){if(1&e&&(t.ynx(0),t.YNc(1,v,13,10,"div",6),t.BQk()),2&e){const o=n.$implicit;t.xp6(1),t.Q6J("ngIf",void 0===o.parent&&o.count)}}function C(e,n){1&e&&(t.TgZ(0,"div"),t._uU(1,"No ad found that matches the search."),t.qZA())}const S=[{path:"",component:(()=>{class e{constructor(o,s){this.shopService=o,this.route=s}ngOnInit(){this.shopService.resetShopParams(),this.route.queryParams.subscribe(o=>{this.searcText=o["search-term"],this.shopService.shopParams.search=o["search-term"],this.getProducts()})}getProducts(){this.shopService.getCategories().pipe((0,l.z)(o=>(this.allCategories=o,this.shopService.getProducts()))).subscribe(o=>{this.totalCount=o.totalCount,this.categoryGroupCountList=o.categoryGroupCount,this.shopService.calculateProductCountsByCategory(this.allCategories,this.categoryGroupCountList)})}}return e.\u0275fac=function(o){return new(o||e)(t.Y36(h.d),t.Y36(r.gz))},e.\u0275cmp=t.Xpm({type:e,selectors:[["app-search-result"]],decls:5,vars:3,consts:[["class","h5",4,"ngIf"],[1,"row"],[4,"ngFor","ngForOf"],[4,"ngIf"],[1,"h5"],[1,"fw-bold",2,"color","brown"],["class","col-4 col-md-2",4,"ngIf"],[1,"col-4","col-md-2"],[1,"card","h-100"],[1,"card-body","text-center"],["alt","",1,"text-center",2,"width","50px",3,"src"],[1,"card-text"],[1,"small","link-dark",3,"routerLink","state"],["class","small text-start",4,"ngFor","ngForOf"],[1,"small","text-start"],["class","small link-dark",3,"routerLink","state",4,"ngIf"]],template:function(o,s){1&o&&(t.YNc(0,p,6,2,"div",0),t.TgZ(1,"div",1),t._UZ(2,"hr"),t.YNc(3,x,2,1,"ng-container",2),t.qZA(),t.YNc(4,C,2,0,"div",3)),2&o&&(t.Q6J("ngIf",s.totalCount),t.xp6(3),t.Q6J("ngForOf",s.allCategories),t.xp6(1),t.Q6J("ngIf",!s.totalCount))},dependencies:[i.sg,i.O5,r.rH]}),e})()}];let _=(()=>{class e{}return e.\u0275fac=function(o){return new(o||e)},e.\u0275mod=t.oAB({type:e}),e.\u0275inj=t.cJS({imports:[r.Bz.forChild(S),r.Bz]}),e})(),Z=(()=>{class e{}return e.\u0275fac=function(o){return new(o||e)},e.\u0275mod=t.oAB({type:e}),e.\u0275inj=t.cJS({imports:[i.ez,_]}),e})()}}]);