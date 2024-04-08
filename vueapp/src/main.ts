// import './assets/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'
import axios from './plugins/axios'
import vuetify from './plugins/vuetify'
import { loadFonts } from './plugins/webfontloader'

loadFonts()

const app = createApp(App)
const baseApiUrl = import.meta.env.VITE_Api_Url;
console.log('baseApiUrl')
console.log(baseApiUrl)
app.use(createPinia())
app.use(router)
app.use(vuetify)
app.mount('#app')
app.use(axios, { baseUrl: baseApiUrl })

//https://blog.logrocket.com/how-to-use-axios-vue-js/