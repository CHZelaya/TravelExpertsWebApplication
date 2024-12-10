function previewImage(event) {
    const file = event.target.files[0];
    const imagePreview = document.querySelector('.editableProfilePic');
    if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
            imagePreview.src = e.target.result;
        }
        reader.readAsDataURL(file);
        //???????
    }
}


//check if user added pic if true display delete option

document.addEventListener('DOMContentLoaded', () => {
    document.querySelector("#profilePicture").addEventListener("change", newImgLoad);


    function newImgLoad() {
        const profilePicSrcVal = document.querySelector("#profilePicture").value;
        if (profilePicSrcVal!='') {            
            // display delete option
            document.querySelector('.deletePPBtn').style.display = "block";

        } else {
            //hide it
            document.querySelector('.deletePPBtn').style.display = "none";
        }
    }
});