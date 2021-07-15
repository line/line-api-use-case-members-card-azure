using System.Collections.Generic;
using MembersCard.Common;

namespace MembersCard.Model
{
    public class FlexMessagePayload
    {
        public string Type { get; set; }
        public string AltText { get; set; }
        public FlexMessageContents Contents { get; set; }

        public FlexMessagePayload() { }

        public FlexMessagePayload(ModifiedProduct product, string language, string liffId)
        {
            Type = "flex";
            AltText = CommonConst.MESSAGE_ALT_TEXT[language];
            Contents = new FlexMessageContents
            {
                Type = "bubble",
                Header = new Header
                {
                    Type = "box",
                    Layout = "vertical",
                    Contents = new List<Content>
                    {
                        new Content
                        {
                            Type = "text",
                            Text = "Use Case STORE",
                            Size = "xxl",
                            Weight = "bold"
                        },
                        new Content
                        {
                            Type = "text",
                            Text = product.Date,
                            Color = "#767676"
                        },
                        new Content
                        {
                            Type = "text",
                            Wrap = true,
                            Text = CommonConst.MESSAGE_NOTES[language],
                            Color = "#ff6347"
                        }
                    }
                },
                Body = new Body
                {
                    Type = "box",
                    Layout = "vertical",
                    PaddingTop = "0%",
                    Contents = new List<Content>
                    {
                        new Content
                        {
                            Type = "box",
                            Layout = "vertical",
                            Margin = "lg",
                            Spacing = "sm",
                            PaddingBottom = "xxl",
                            Contents = new List<Content>
                            {
                                new Content
                                {
                                    Type = "box",
                                    Layout = "baseline",
                                    Spacing = "sm",
                                    Contents = new List<Content>
                                    {
                                        new Content
                                        {
                                            Type = "text",
                                            Text = product.ProductName,
                                            Color = "#5B5B5B",
                                            Size = "sm",
                                            Flex = 5
                                        },
                                        new Content
                                        {
                                            Type = "text",
                                            Text = product.ProductPrice,
                                            Wrap = true,
                                            Color = "#666666",
                                            Size = "sm",
                                            Flex = 2,
                                            Align = "end"
                                        }
                                    }
                                },
                                new Content
                                {
                                    Type = "box",
                                    Layout = "baseline",
                                    Spacing = "sm",
                                    Contents = new List<Content>
                                    {
                                        new Content
                                        {
                                            Type = "text",
                                            Text = CommonConst.MESSAGE_POSTAGE[language],
                                            Color = "#5B5B5B",
                                            Size = "sm",
                                            Flex = 5
                                        },
                                        new Content
                                        {
                                            Type = "text",
                                            Text = product.Postage,
                                            Wrap = true,
                                            Color = "#666666",
                                            Size = "sm",
                                            Flex = 2,
                                            Align = "end"
                                        }
                                    }
                                },
                                new Content
                                {
                                    Type = "box",
                                    Layout = "baseline",
                                    Spacing = "sm",
                                    Contents = new List<Content>
                                    {
                                        new Content
                                        {
                                            Type = "text",
                                            Text = CommonConst.MESSAGE_FEE[language],
                                            Color = "#5B5B5B",
                                            Size = "sm",
                                            Flex = 5
                                        },
                                        new Content
                                        {
                                            Type = "text",
                                            Text = product.Fee,
                                            Wrap = true,
                                            Color = "#666666",
                                            Size = "sm",
                                            Flex = 2,
                                            Align = "end"
                                        }
                                    }
                                },
                                new Content
                                {
                                    Type = "box",
                                    Layout = "baseline",
                                    Spacing = "sm",
                                    Contents = new List<Content>
                                    {
                                        new Content
                                        {
                                            Type = "text",
                                            Text = CommonConst.MESSAGE_DISCOUNT[language],
                                            Color = "#5B5B5B",
                                            Size = "sm",
                                            Flex = 5
                                        },
                                        new Content
                                        {
                                            Type = "text",
                                            Text = product.Discount,
                                            Wrap = true,
                                            Color = "#666666",
                                            Size = "sm",
                                            Flex = 2,
                                            Align = "end"
                                        }
                                    }
                                },
                                new Content
                                {
                                    Type = "box",
                                    Layout = "baseline",
                                    Spacing = "sm",
                                    Contents = new List<Content>
                                    {
                                        new Content
                                        {
                                            Type = "text",
                                            Text = CommonConst.MESSAGE_SUBTOTAL[language],
                                            Color = "#5B5B5B",
                                            Size = "sm",
                                            Flex = 5
                                        },
                                        new Content
                                        {
                                            Type = "text",
                                            Text = product.Subtotal,
                                            Wrap = true,
                                            Color = "#666666",
                                            Size = "sm",
                                            Flex = 2,
                                            Align = "end"
                                        }
                                    }
                                },
                                new Content
                                {
                                    Type = "box",
                                    Layout = "baseline",
                                    Spacing = "sm",
                                    Contents = new List<Content>
                                    {
                                        new Content
                                        {
                                            Type = "text",
                                            Text = CommonConst.MESSAGE_TAX[language],
                                            Color = "#5B5B5B",
                                            Size = "sm",
                                            Flex = 5
                                        },
                                        new Content
                                        {
                                            Type = "text",
                                            Text = product.Tax,
                                            Wrap = true,
                                            Color = "#666666",
                                            Size = "sm",
                                            Flex = 2,
                                            Align = "end"
                                        }
                                    }
                                },
                                new Content
                                {
                                    Type = "box",
                                    Layout = "baseline",
                                    Spacing = "sm",
                                    Contents = new List<Content>
                                    {
                                        new Content
                                        {
                                            Type = "text",
                                            Text = CommonConst.MESSAGE_TOTAL[language],
                                            Color = "#5B5B5B",
                                            Size = "sm",
                                            Flex = 5
                                        },
                                        new Content
                                        {
                                            Type = "text",
                                            Text = product.Total,
                                            Wrap = true,
                                            Color = "#666666",
                                            Size = "sm",
                                            Flex = 2,
                                            Align = "end"
                                        }
                                    }
                                },
                                new Content
                                {
                                    Type = "box",
                                    Layout = "baseline",
                                    Spacing = "sm",
                                    Contents = new List<Content>
                                    {
                                        new Content
                                        {
                                            Type = "text",
                                            Text = CommonConst.MESSAGE_AWARD_POINTS[language],
                                            Color = "#5B5B5B",
                                            Size = "sm",
                                            Flex = 5
                                        },
                                        new Content
                                        {
                                            Type = "text",
                                            Text = product.Point,
                                            Wrap = true,
                                            Color = "#666666",
                                            Size = "sm",
                                            Flex = 2,
                                            Align = "end"
                                        }
                                    }
                                }
                            }
                        },
                        new Content
                        {
                            Type = "box",
                            Layout = "vertical",
                            Contents = new List<Content>
                            {
                                new Content
                                {
                                    Type = "text",
                                    Text = CommonConst.MESSAGE_THANKS[language],
                                    Wrap = true,
                                    Size = "sm",
                                    Color = "#767676"
                                }
                            }
                        },
                        new Content
                        {
                            Type = "box",
                            Layout = "vertical",
                            Contents = new List<Content>
                            {
                                new Content
                                {
                                    Type = "image",
                                    Url = product.ImgUrl,
                                    Size = "lg"
                                }
                            },
                            Margin = "xxl"
                        }
                    }
                },
                Footer = new Footer
                {
                    Type = "box",
                    Layout = "vertical",
                    Spacing = "sm",
                    Contents = new List<Content>
                    {
                        new Content()
                        {
                            Type = "button",
                            Style = "link",
                            Height = "sm",
                            Action = new Action()
                            {
                                Type = "uri",
                                Label = CommonConst.MESSAGE_VIEW[language],
                                Uri = $"https://liff.line.me/{liffId}?lang={language}"
                            },
                            Color = "#0033cc"
                        },
                        new Content()
                        {
                            Type = "spacer",
                            Size = "md"
                        }
                    },
                    Flex = 0
                }
            };
        }
        public class FlexMessageContents
        {
            public string Type { get; set; }
            public Header Header { get; set; }
            public Body Body { get; set; }
            public Footer Footer { get; set; }
        }

        public class Header
        {
            public string Type { get; set; }
            public string Layout { get; set; }
            public IEnumerable<Content> Contents { get; set; }
        }

        public class Body
        {
            public string Type { get; set; }
            public string Layout { get; set; }
            public string PaddingTop { get; set; }
            public IEnumerable<Content> Contents { get; set; }
        }

        public class Footer
        {
            public string Type { get; set; }
            public string Layout { get; set; }
            public string Spacing { get; set; }
            public int? Flex { get; set; }
            public IEnumerable<Content> Contents { get; set; }
        }

        public class Content
        {
            public string Type { get; set; }
            public string Layout { get; set; }
            public string Text { get; set; }
            public string Weight { get; set; }
            public string Size { get; set; }
            public string Color { get; set; }
            public bool? Wrap { get; set; }
            public string Spacing { get; set; }
            public string Margin { get; set; }
            public int? Flex { get; set; }
            public string Align { get; set; }
            public string PaddingBottom { get; set; }
            public string Url { get; set; }
            public string Style { get; set; }
            public string Height { get; set; }
            public Action Action { get; set; }
            public IEnumerable<Content> Contents { get; set; }
        }

        public class Action
        {
            public string Type { get; set; }
            public string Label { get; set; }
            public string Uri { get; set; }
        }
    }
}
