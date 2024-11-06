<script setup>
 import NavBar from '../src/components/NavBar.vue';

</script>

<template>
    <div class="main">
      <router-view></router-view>
    </div>

    <NavBar/>
</template>

<script>
export default {
  name: 'new-post',
  data() {
    return {
      submitted: false,
      post: {
        title: "",
        body: ""
      }
    };
  }, 
  methods: {
    savePost() {
      var data = {
        title: this.title,
        body: this.body,
        userId: 1
      };

      console.log(data);

      PostDataService.create(data)
      .then(response => {
        this.submitted = true;
        console.log(response.data);
      })
      .catch(e => {
        console.log(e);
      });
    },

    newPost() {
      this.submitted = false;
      this.post = {};
    }
  }
}
</script>