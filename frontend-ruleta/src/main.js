import { createApp } from 'vue'
import App from './App.vue'
import { Roulette } from 'vue3-roulette'
import { createPinia } from 'pinia'
import './style.css'

// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

const vuetify = createVuetify({
    components,
    directives,
})

const app = createApp(App)
app.component('Roulette', Roulette)
app.use(createPinia())
app.use(vuetify)
app.mount('#app')
