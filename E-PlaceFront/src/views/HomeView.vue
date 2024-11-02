<script setup>
</script>

<template>
  <main class="fixed-top" id="main1">
    <div id="carouselExampleCaptions" class="carousel slide" data-bs-ride="carousel" data-bs-interval="3000">
      <div class="carousel-indicators">
        <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
        <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="1" aria-label="Slide 2"></button>
        <button type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide-to="2" aria-label="Slide 3"></button>
      </div>
      <div class="carousel-inner">
        <div class="carousel-item active">
          <img src="../assets/img/Tela 1.png" class="d-block w-100" alt="imagem1">
        </div>
        <div class="carousel-item">
          <img src="../assets/img/Tela 2.png" class="d-block w-100" alt="imagem2">
        </div>
        <div class="carousel-item">
          <img src="../assets/img/Tela 3.png" class="d-block w-100" alt="imagem3">
        </div>
      </div>
      <button class="carousel-control-prev setas-button" type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide="prev">
        <span class="carousel-control-prev-icon setas-carousel" aria-hidden="true"></span>
        <span class="visually-hidden">Previous</span>
      </button>
      <button class="carousel-control-next setas-button" type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide="next">
        <span class="carousel-control-next-icon setas-carousel" aria-hidden="true"></span>
        <span class="visually-hidden">Next</span>
      </button>
    </div>
  </main> 
</template>

<style>
        #main1 {
  height: 100vh;
}

/* Carousel to occupy full height */
.carousel-inner {
  height: 100vh;
}

/* Carousel items and images */
.carousel-item,
.carousel-item img {
  height: 100vh;
  width: 100%;
  object-fit: cover; /* Ensures images cover the entire area */
}

.carousel-caption h5 {
  color: white;
  font-weight: bold;
}

.carousel-caption p {
  color: white;
}

/* Hover effect for carousel controls */
.setas-carousel:hover {
  transform: scale(1.1);
  transition: 0.5s;
}

/* Adjust carousel control button positioning */
.carousel-control-prev,
.carousel-control-next {
  height: 100%;
  top: 0;
  bottom: 0;
  display: flex;
  align-items: center;
}

.carousel-control-prev-icon,
.carousel-control-next-icon {
  width: 3rem;
  height: 3rem;
}

.carousel-control-prev-icon,
.carousel-control-next-icon {
  background-size: 100%, 100%;
}
    </style>


