export function parseJwt(token) {
    try {
        const payload = token.split(".")[1];
        const json = atob(payload.replace(/-/g, "+").replace(/_/g, "/"));
        return JSON.parse(json);
    } catch {
        return null;
    }
}

export function getRoleFromPayload(p) {
    if (!p) return null;
    return (
        p["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] ||
        p.role ||
        null
    );
}

export function getNameFromPayload(p) {
    if (!p) return null;
    return (
        p["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"] ||
        p.unique_name ||
        p.name ||
        null
    );
}
