var toggle = document.querySelectorAll(".nav-header")
var display = document.querySelectorAll(".nav .tree")
var arrow = document.querySelectorAll(".fa-solid.fa-angle-up")
var icon = document.querySelectorAll(".icon");

console.log(arrow[0].classList.contains("fa-angle-down"));

for(let i = 0; i < toggle.length; i++){
  toggle[i].addEventListener('click',()=>{

      toggle[i].classList.toggle("orange");


      if(display[i].style.display == "none" ){

        display[i].style.display = "block";
        arrow[i].classList.toggle("fa-angle-up");
        arrow[i].classList.toggle("fa-angle-down");
        icon[i].classList.toggle("orange");
      }
      else{
        display[i].style.display="none";
        icon[i].classList.toggle("orange");
        arrow[i].classList.toggle("fa-angle-down");
        arrow[i].classList.toggle("fa-angle-up");


      }



  });
}


var productList =[
  {
    name: "Urbas SC - Mule",
    color: "Dusty Blue",
    price: 620000,
    img: "https://ananas.vn/wp-content/uploads/Pro_AV00202_1-500x500.jpg"
  },
  {
    name: "Urbas SC - Mule",
    color: "Dusty Blue",
    price: 620000,
    img: "https://ananas.vn/wp-content/uploads/Pro_AV00202_1-500x500.jpg"
  },
  {
    name: "Urbas SC - Mule",
    color: "Dusty Blue",
    price: 620000,
    img: "https://ananas.vn/wp-content/uploads/Pro_AV00202_1-500x500.jpg"
  },
  {
    name: "Urbas SC - Mule",
    color: "Dusty Blue",
    price: 620000,
    img: "https://ananas.vn/wp-content/uploads/Pro_AV00202_1-500x500.jpg"
  },
  {
    name: "Urbas SC - Mule",
    color: "Dusty Blue",
    price: 620000,
    img: "https://ananas.vn/wp-content/uploads/Pro_AV00202_1-500x500.jpg"
  },
  {
    name: "Urbas SC - Mule",
    color: "Dusty Blue",
    price: 620000,
    img: "https://ananas.vn/wp-content/uploads/Pro_AV00202_1-500x500.jpg"
  },
  {
    name: "Urbas SC - Mule",
    color: "Dusty Blue",
    price: 620000,
    img: "https://ananas.vn/wp-content/uploads/Pro_AV00202_1-500x500.jpg"
  }
];


productList.forEach(i => {

});