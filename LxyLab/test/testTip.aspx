<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testTip.aspx.cs" Inherits="LxyLab.test.testTip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title> 
    <link href="../source/qTip2/tips/jquery.qtip.nightly.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../source/easyui/jquery-1.8.0.min.js"></script> 
    <!--link type="text/css" href="../source/qTip2/tips/jquery.qtip.min.css" rel="stylesheet" /-->
    <script  type="text/javascript" src="../source/qTip2/tips/jquery.qtip.nightly.min.js"></script>
</head>
<body>
    <a id="demo2" title="哈哈哈">ss</a>
     

    <div class="hasTooltip" style="width:120px;">Hover me to see a tooltip</div>
    <div class="hidden"> <!-- This class should hide the element, change it if needed -->
	    <p><b>Complex HTML</b> for your tooltip <i>here</i>!</p>
    </div>  
    <div id="demo-tips" >
	    <div class="structure" style="width:320px; margin-left:200px; height:300px;"> </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $('[title]').qtip();
            $('.hasTooltip').each(function () { // Notice the .each() loop, discussed below
                $(this).qtip({
                    content: {
                        text: 'At its fff' ,
                        title: {
                            text: 'My fff',
                            button: true
                        }
                    },
                    position: {
                        my: "right bottom", // Use the corner...
                        at: "bottom center" // ...and opposite corner
                    },
                    //show: {
                    //    event: true, // Don't specify a show event...
                    //    ready: true // ... but show the tooltip when ready
                    //},
                    hide: false, // Don't specify a hide event either!
                    style: {
                        classes: 'qtip-shadow qtip-bootstrap'
                    }
                });
                
            });
            //$("a").qtip({
            //    content: "这是提示内容（by囧月）"
            //    , title: {
            //        text: "提示标题"
            //        , button: "关闭"
            //    }
            //});


            // Define our positioning and style arrays
            var at = [
                    'bottom left', 'bottom right', 'bottom center',
                    'top left', 'top right', 'top center',
                    'left center', 'left top', 'left bottom',
                    'right center', 'right top', 'right bottom', 'center'
            ],
                my = [
                    'top left', 'top right', 'top center',
                    'bottom left', 'bottom right', 'bottom center',
                    'right center', 'right top', 'right bottom',
                    'left center', 'left top', 'left bottom', 'center'
                ],
                styles = [
                    'red', 'blue', 'dark', 'light', 'green', 'jtools', 'plain', 'youtube', 'cluetip', 'tipsy', 'tipped', 'bootstrap'
                ];

            // Create the tooltips only on document load
            $(document).ready(function () {
                // Loop through the my array
                for (var i = 0; i < my.length; i++) {
                    $('.structure')
                        /*
                         * Lets delete the qTip data from our target element so we can apply multiple tooltips.
                         * Since the data is also stored on the tooltip element itself this isn't a problem!
                         * 
                         * Check here for more details on this: http://craigsworks.com/projects/qtip2/tutorials/advanced/#multi
                         */
                        .removeData('qtip')
                        .qtip({
                            content: {
                                text: 'At its ' + at[i],
                                title: {
                                    text: 'My ' + my[i],
                                    button: true
                                }
                            },
                            position: {
                                my: my[i], // Use the corner...
                                at: at[i] // ...and opposite corner
                            },
                            show: {
                                event: false, // Don't specify a show event...
                                ready: true // ... but show the tooltip when ready
                            },
                            hide: false, // Don't specify a hide event either!
                            style: {
                                classes: 'qtip-shadow qtip-' + styles[i]
                            }
                        });
                }
            });


        });
        </script>
</body>
</html>
