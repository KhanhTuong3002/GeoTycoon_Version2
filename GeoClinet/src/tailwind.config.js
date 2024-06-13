/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["**/*.cshtml"],
    theme: {
        extend: {
            fontFamily: {
                sans: ['Poppins', 'Rubik', '-apple-system', 'BlinkMacSystemFont', 'Segoe UI', 'Helvetica Neue', 'Arial', 'sans-serif', 'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol'],
            },
            colors: {
                bodyColor: '#2F281E',
                backgroundColor: '#FFFEFE',
            },
        },
    },
    plugins: [require("daisyui")],
    daisyui: {
        themes: [
            {
                "ayu-light": {
                    "primary": "#747DEF",
                    "secondary": "#544837",
                    "accent": "#ffbf00",
                    "neutral": "#F0F4F9",
                    "base-100": "#FCFCFC",
                    "base-200": "#F3F4F5",
                    "base-300": "#ACB6BF",
                    "info": "#567592",
                    "success": "#79B93C",
                    "warning": "#FF9900",
                    "error": "#DF6951",
                },
            },
        ],
    },
}

