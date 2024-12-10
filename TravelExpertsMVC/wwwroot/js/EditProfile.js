function previewImage(event) {
    const file = event.target.files[0];
    const imagePreview = document.querySelector('.editableProfilePic');
    if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
            imagePreview.src = e.target.result;
        }
        reader.readAsDataURL(file);
    }
}