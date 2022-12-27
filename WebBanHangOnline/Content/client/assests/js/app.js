
//Variable
//Handle favorite item
let favoriteList = []; //Wish list
let btnFavorite = document.querySelector(".btn-favorite")

btnFavorite.addEventListener("click",addToFavList);

//Add item to wish list function
function addToFavList(){
    let favItem = document.getElementById("favorite-item")
    favItem.classList.toggle("isFavorite");

    if(favItem.classList.contains("isFavorite")){
        favoriteList.push("Hello");
    }
    else favoriteList.pop("Hello");
}
 

//Handle FYI
const accordian = document.getElementsByClassName('content-container')
for(let i = 0; i < accordian.length;i++){
    accordian[i].addEventListener('click',function(){
        this.classList.toggle('active');
    })
}

//Handle image slider 
//Variables
let Images = document.getElementsByClassName("small-img");
let activeImages = document.getElementsByClassName("active-img");

for(var i = 0;i < Images.length ; i++){
    Images[i].addEventListener('mouseover',function(){
        this.classList.add('active-img');
        document.getElementById('featured-img').src = this.src;
    })
}
