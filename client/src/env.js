// _env.js_
export const dev = window.location.origin.includes("localhost");
export const baseURL = dev ? 'https://localhost:7045' : '';
export const useSockets = false;
export const domain = "dev-bhtkdl8nso7vkz4z.us.auth0.com";
export const allSpiceApi = "https://localhost:7045";
export const clientId = "6oc9O5nQoD1TOTYOlfAmhrTLalXzKiJx";
export const audience = "https://loginapi.com";