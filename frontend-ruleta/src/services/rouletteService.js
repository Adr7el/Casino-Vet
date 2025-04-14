import axios from 'axios'

const API_URL = 'https://localhost:44346/api'

export async function getUsuarioPorNombre(nombre) {
    try {
        const response = await axios.get(`${API_URL}/Usuarios/${nombre}`)
        return response.data
    } catch (error) {
        if (error.response && error.response.status === 404) {
            throw error.response.data.message || 'Error al buscar usuario'
        } else {
            throw error.message || 'Error desconocido'
        }
    }
}

export async function apostarBackend(payload) {
    try {
        const response = await axios.post(`${API_URL}/Ruleta/apostar`, payload)
        return response.data
    } catch (error) {
        throw error
    }
}

export async function guardarUsuario(payload) {
    try {
        const response = await axios.post(`${API_URL}/Usuarios/guardar`, payload)
        return response.data
    } catch (error) {
        throw error.response?.data?.message || 'Error al guardar el usuario'
    }
}

