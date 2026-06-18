// --- Бургер-меню ---
const burgerBtn = document.getElementById('burger-btn');
const mobileMenu = document.getElementById('mobile-menu');
const burgerIcon = document.getElementById('burger-icon');
const closeIcon = document.getElementById('close-icon');
let isMenuOpen = false;

if (burgerBtn && mobileMenu) {
    burgerBtn.addEventListener('click', function () {
        isMenuOpen = !isMenuOpen;

        if (isMenuOpen) {
            mobileMenu.style.maxHeight = mobileMenu.scrollHeight + 'px';
            mobileMenu.style.opacity = '1';
            burgerIcon.classList.add('hidden');
            closeIcon.classList.remove('hidden');
        } else {
            closeMobileMenu();
        }

        burgerBtn.setAttribute('aria-expanded', isMenuOpen);
    });
}

function closeMobileMenu() {
    if (!mobileMenu || !burgerBtn) return;

    isMenuOpen = false;
    mobileMenu.style.maxHeight = '0';
    mobileMenu.style.opacity = '0';
    burgerIcon.classList.remove('hidden');
    closeIcon.classList.add('hidden');
    burgerBtn.setAttribute('aria-expanded', 'false');
}

function openCallbackModal() {
    const name = prompt('Введите ваше имя:');
    if (!name) return;

    const phone = prompt('Введите ваш телефон:');
    if (!phone) return;

    alert('Спасибо, ' + name + '! Мы перезвоним вам на номер ' + phone + ' в ближайшее время.');
}
