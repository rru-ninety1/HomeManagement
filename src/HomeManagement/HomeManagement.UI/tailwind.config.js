/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["*.{html,js,razor,razor.cs}", "**/*.{html,js,razor,razor.cs}"],
    safelist: [
        "h-screen",
        "bg-gray-100",
        "max-w-sm",
        "mx-auto"
    ],
    theme: {
        extend: {},
    },
    plugins: [],

}
