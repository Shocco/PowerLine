﻿using System;
using System.Collections.Generic;
using System.Management.Automation.Host;
using System.Globalization;
using System.Threading;
using System.Security;
using System.Management.Automation;
using System.Collections.ObjectModel;

namespace PowerLine
{
    //http://community.bartdesmet.net/blogs/bart/archive/2008/07/06/windows-powershell-through-ironruby-writing-a-custom-pshost.aspx
    //http://stackoverflow.com/questions/1233640/capturing-powershell-output-in-c-sharp-after-pipeline-invoke-throws
    internal class MyPSHost : PSHost
    {
        private Guid _hostId;
        private MyPSHostUI _ui;

        public MyPSHost()
        {
            _hostId = Guid.NewGuid();
            _ui = new MyPSHostUI();
        }

        public override Guid InstanceId
        {
            get { return _hostId; }
        }

        public override string Name
        {
            get { return "MyPSHost"; }
        }

        public override Version Version
        {
            get { return new Version(1, 0); }
        }
        public override PSHostUserInterface UI
        {
            get { return _ui; }
        }

        public override CultureInfo CurrentCulture
        {
            get { return Thread.CurrentThread.CurrentCulture; }
        }

        public override CultureInfo CurrentUICulture
        {
            get { return Thread.CurrentThread.CurrentUICulture; }
        }

        public override void EnterNestedPrompt()
        {
            return;
        }

        public override void ExitNestedPrompt()
        {
            return;
        }

        public override void NotifyBeginApplication()
        {
            return;
        }

        public override void NotifyEndApplication()
        {
            return;
        }

        public override void SetShouldExit(int exitCode)
        {
            return;
        }
    }

    internal class MyPSHostUI : PSHostUserInterface
    {
        private MyPSHostRawUI _rawui;
        private List<string> _sb;

        public MyPSHostUI()
        {
            _rawui = new MyPSHostRawUI();
            _sb = new List<string>();
        }
        public override PSHostRawUserInterface RawUI { get { return _rawui; } }

        public override Dictionary<string, PSObject> Prompt(string caption, string message, Collection<FieldDescription> descriptions)
        {
            return null;
        }

        public List<string> Output
        {
            get
            {
                return _sb;
            }
        }

        public override int PromptForChoice(string caption, string message, Collection<ChoiceDescription> choices, int defaultChoice)
        {
            return 0;
        }

        public override PSCredential PromptForCredential(string caption, string message, string userName, string targetName)
        {
            return null;
        }

        public override PSCredential PromptForCredential(string caption, string message, string userName, string targetName, PSCredentialTypes allowedCredentialTypes, PSCredentialUIOptions options)
        {
            return null;
        }

        public override string ReadLine()
        {
            return null;
        }

        public override SecureString ReadLineAsSecureString()
        {
            return null;
        }

        public override void Write(string value)
        {
            _sb.Add(value);
        }

        public override void Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string value)
        {
            _sb.Add(value);
            _rawui.BackgroundColor = backgroundColor;
            _rawui.ForegroundColor = foregroundColor;
        }

        public override void WriteDebugLine(string value)
        {
            _sb.Add(value + "\r\n");
        }

        public override void WriteErrorLine(string value)
        {
            _sb.Add(value + "\r\n");
        }

        public override void WriteLine(string value)
        {
            _sb.Add(value + "\r\n");
        }

        public override void WriteProgress(long sourceId, ProgressRecord record)
        {
            return;
        }

        public override void WriteVerboseLine(string value)
        {
            _sb.Add(value + "\r\n");
        }

        public override void WriteWarningLine(string value)
        {
            _sb.Add(value + "\r\n");
        }
    }

    internal class MyPSHostRawUI : PSHostRawUserInterface
    {
        private ConsoleColor _bgColor;
        private ConsoleColor _fgColor;
        private Size _bufSize;

        public MyPSHostRawUI()
        {
            _bgColor = ConsoleColor.Black;
            _fgColor = ConsoleColor.Yellow;
            _bufSize = new Size(80, 60);
        }

        public override ConsoleColor BackgroundColor
        {
            get
            {
                return _bgColor;
            }

            set
            {
                _bgColor = value;
            }
        }

        public override Size BufferSize
        {
            get
            {
                return _bufSize;
            }

            set
            {
                _bufSize = value;
            }
        }

        public override Coordinates CursorPosition
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override int CursorSize
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override ConsoleColor ForegroundColor
        {
            get
            {
                return _fgColor;
            }

            set
            {
                _fgColor = value;
            }
        }

        public override bool KeyAvailable
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override Size MaxPhysicalWindowSize
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override Size MaxWindowSize
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override Coordinates WindowPosition
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override Size WindowSize
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override string WindowTitle
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void FlushInputBuffer()
        {
            throw new NotImplementedException();
        }

        public override BufferCell[,] GetBufferContents(Rectangle rectangle)
        {
            throw new NotImplementedException();
        }

        public override KeyInfo ReadKey(ReadKeyOptions options)
        {
            throw new NotImplementedException();
        }

        public override void ScrollBufferContents(Rectangle source, Coordinates destination, Rectangle clip, BufferCell fill)
        {
            throw new NotImplementedException();
        }

        public override void SetBufferContents(Rectangle rectangle, BufferCell fill)
        {
            throw new NotImplementedException();
        }

        public override void SetBufferContents(Coordinates origin, BufferCell[,] contents)
        {
            throw new NotImplementedException();
        }
    }
}
