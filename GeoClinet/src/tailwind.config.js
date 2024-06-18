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
    plugins: [

        require('daisyui'),
    ],
    daisyui: {
        darkTheme: "ayu-dark",
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
                "ayu-dark": {
                    "primary": "#39BAE6",
                    "secondary": "#7FD962",
                    "accent": "#E6B450",
                    "neutral": "#ACB6BF",
                    "base-100": "#0F131A",
                    "base-200": "#0D1017",
                    "base-300": "#0B0E14",
                    "info": "#59C2FF",
                    "success": "#7FD962",
                    "warning": "#FFB454",
                    "error": "#D95757",
                },
                "catpuccin-macchiato": {
                    "primary": "#91d7e3",
                    "secondary": "#a6da95",
                    "accent": "#f5bde6",
                    "neutral": "#6e738d",
                    "base-100": "#24273a",
                    "base-200": "#1e2030",
                    "base-300": "#181926",
                    "info": "#91d7e3",
                    "success": "#a6da95",
                    "warning": "#eed49f",
                    "error": "#ee99a0",

                },
            },
        ],
    },
}

