var createRoomButton = document.getElementById('btn-create-room')
var createRoomModal = document.getElementById('modal-create-room');
createRoomButton.addEventListener('click', function () {
    createRoomModal.classList.add('active')

});

var CloseModal=function () {
    createRoomModal.classList.remove('active')
}