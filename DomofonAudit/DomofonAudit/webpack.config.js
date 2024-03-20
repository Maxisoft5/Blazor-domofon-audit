const glob = require("glob");
const path = require("path");
const webpack = require("webpack");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

var entriesArray = glob.sync("./wwwroot/js/*.ts") || [];
// add your entry entriesArray.push('yourpath');
module.exports = {
    mode: "development",
    devtool: "source-map",
    entry: {
        scripts: {
            import: entriesArray,
            filename: "scripts.bundle.js",
        },
        style: {
            import: ["./wwwroot/css/all_styles.scss"],
            filename: "style-delete.bundle.css",
        },
    },
    output: {
        filename: "[name]",
        path: path.resolve(__dirname, "wwwroot/dist"),
        clean: true,
    },
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                use: "ts-loader",
                exclude: /node_modules/,
            },
            {
                test: /\.s[ac]ss$/i,
                use: [
                    {
                        loader: MiniCssExtractPlugin.loader,
                        options: {
                            publicPath: "",
                        },
                    },
                    {
                        loader: "css-loader", // translates CSS into CommonJS modules
                    },
                    {
                        loader: "postcss-loader",
                        options: {
                            postcssOptions: {
                                plugins: () => [require("autoprefixer")],
                            },
                        },
                    },
                    {
                        loader: "sass-loader", // compiles SASS to CSS
                    },
                ],
            },
        ],
    },
    resolve: {
        extensions: [".tsx", ".ts", ".js"],
    },
    plugins: [
        new webpack.ProvidePlugin({
            $: "jquery",
            jQuery: "jquery",
            "window.jQuery": "jquery",
        }),
        new MiniCssExtractPlugin({
            filename: "[name].css",
        }),
    ],
};
