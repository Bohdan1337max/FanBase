export const StoreJwt = async (apiUrl, requestOption) => {
    try {
        const response = await fetch(apiUrl, requestOption);

        if (!response.ok) {
            const errorData = await response.json();
            console.error("Error:", errorData);
            return false;
        }

        const data = await response.json();
        console.log("Fetched Data", data)

        const token = data.token;

        if (token) {
            localStorage.setItem('jwtToken', token);
            console.log("Token stored in localStorage:", token);
            return true;
        } else {
            console.error("No token received");
            return false
        }
    } catch (error) {
        console.error("Fetch error:", error)
        return false;
    }
}

export const Logout = () => {
    localStorage.removeItem('jwtToken');

}